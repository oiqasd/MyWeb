using System;
using YZ.Common.Log;
using YZ.Common.Util;
using YZ.Service.Model.DTO;



namespace YZ.Service.WebApi
{
    public class ServiceHandler : ServiceStack.Service
    {
        public ServiceResponse Any(ServiceRequest request)
        {
            LogHelper.Info("服务类接口收到请求", Request.RemoteIp + "\t" + JsonHelper.Serialize(request));




            return null;
        }


        /*
        /// <summary>
        /// 此接口提供认证类等有敏感数据使用				
        /// </summary>
        public SensitiveMessageResponse Any(SensitiveMessageRequest request)
        {
            IAcDomain acDomain = new AcDomain();
            acDomain.StartTime = Request.StartTime;
            SensitiveMessageResponse response = new SensitiveMessageResponse();
            AppInfoResponse appInfo = new AppInfoResponse();
            string p2pPrivateKey = ConfigHelper.AppSetting<string>("p2pPrivateKey", "");
            string DecAesKey = string.Empty;
            P2PServiceContext context;
            try
            {
                LogHelper.Info("敏感数据接口收到请求", Request.RemoteIp + "\t" + Request.JsonRawBody, acDomain.StartTime);
                acDomain.RemoteIp = Request.RemoteIp;
                ////判断请求权限
                if (!RequestAuthHelper.CheckRequestAuth<SensitiveMessageRequest>(request))
                {
                    response.ResultCode = (int)ResultStatus.AuthorizationError;
                    response.ResultMsg = "没有访问该接口权限";
                    return response;
                }

                string method = request.MessageType;
                string actionName = request.ActionName;
                string actionKey = string.Format("{0}.{1}", method, actionName);
                string data = StringHelper.ConvertToBase64Str(request.Data);
                string aesKey = StringHelper.ConvertToBase64Str(request.Key);
                string version = request.Version;
                string token = request.Token;

                string appId = request.AppId;
                string sign = request.Sign;
                Int64 timeStamp = request.TimeStamp;

                //1、验证app
                if (!string.IsNullOrEmpty(appId))
                    appInfo = AppBusiness.Instance.GetAppInfo(appId);
                //2、检查参数
                if (string.IsNullOrEmpty(method) || string.IsNullOrEmpty(version) || string.IsNullOrEmpty(actionName) || string.IsNullOrEmpty(aesKey) || string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(sign) || timeStamp == 0)
                {
                    response.ResultCode = -1;
                    response.ResultMsg = "缺少参数";
                    return response;
                }
                #region 检查Token
                //修改登录密码,修改支付密码，以及代理商修改登录密码,代理商修改支付密码等接口必须验证token
                if (!RequestAuthHelper.CheckIsNotToken(request))
                {
                    //检查登录token
                    if (!string.IsNullOrEmpty(token))
                    {
                        ResultStatus resultStatus = OAuthBusiness.Instance.CheckToken(token);
                        if (resultStatus == ResultStatus.Ok)
                        {
                            acDomain.CurrentUID = OAuthBusiness.Instance.GetAccountInfo(token).UID;
                        }
                        else
                        {
                            response.ResultCode = (int)ResultStatus.TokenError;
                            response.ResultMsg = "检查token失败";
                            return response;
                        }
                    }
                    else
                    {
                        response.ResultCode = (int)ResultStatus.TokenError;
                        response.ResultMsg = "缺少token";
                        return response;

                    }
                }
                #endregion

                //时间戳验证
                if (!Utils.CheckTimeStamp(timeStamp))
                {
                    response.ResultCode = (int)ResultStatus.InvalidArgument;
                    response.ResultMsg = "时间戳已过期";
                    return response;
                }

                if (appInfo == null || string.IsNullOrEmpty(appInfo.AppId))
                {
                    response.ResultCode = (int)ResultStatus.InvalidArgument;
                    response.ResultMsg = "app验证失败";
                    return response;
                }

                if (appInfo.AppStatus != 20)
                {
                    response.ResultCode = (int)ResultStatus.StatusError;
                    response.ResultMsg = "app状态错误";
                    return response;
                }


                //3、校验签名 RSA
                SortedDictionary<String, Object> dicReq = Utils.EntityToMap(request);
                //组合加密字符串
                string strSign = string.Empty;
                foreach (string key in dicReq.Keys)
                {
                    object o = dicReq[key];
                    if (key.ToLower() == "sign")
                        continue;
                    if (o != null)
                    {
                        if (key.ToLower() == "data" || key.ToLower() == "key")
                            strSign += StringHelper.ConvertToBase64Str(o.ToString());
                        else
                            strSign += o.ToString();
                    }
                }
                sign = StringHelper.ConvertToBase64Str(sign);
                if (!RSAFromPkcs8.checkSign(strSign, sign, appInfo.PublicKey, "UTF-8"))
                {
                    response.ResultCode = (int)ResultStatus.SignError;
                    response.ResultMsg = "RAS验签失败";
                    return response;
                }

                DecAesKey = RSA.RSADecrypt(aesKey, p2pPrivateKey);
                if (string.IsNullOrEmpty(DecAesKey))
                {
                    response.ResultCode = (int)ResultStatus.InvalidArgument;
                    response.ResultMsg = "AesKey解密失败";
                    return response;
                }


                //AES解密请求数据
                string DecData = AES.Decrypt(data, DecAesKey);

                //AES解密data重新赋值
                request.Data = DecData;
                context = new P2PServiceContext(acDomain, P2PServiceRequest.Create(acDomain, request));
                context.AcDomain.CurrentAppId = ConfigHelper.AppSetting<string>("appid");

                //4、执行方法
                DelegateFactory.GetDoAction(actionKey)(context);
                response = context.Response.SensitiveToMessage();
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.Fatal("处理请求出现异常", ex.Message, ex, acDomain.StartTime);
                response.ResultCode = 500;
                response.ResultMsg = "服务器内部逻辑异常";
            }
            finally
            {
                //dataRSA加密
                if (!string.IsNullOrEmpty(response.Data))
                    response.Data = AES.Encrypt(response.Data, DecAesKey);
                else
                    response.Data = "";

                //AesKey加密
                if (!string.IsNullOrEmpty(DecAesKey))
                    response.Key = RSA.RSAEncrypt(DecAesKey, appInfo.PublicKey);
                string respData = response.Data + response.Key + response.ResultCode + response.ResultMsg;
                //生成sign
                response.Sign = RSAFromPkcs8.sign(respData, p2pPrivateKey, "UTF-8");
                if (response.ResultCode == 0)
                    LogHelper.Info("请求处理结束", JsonHelper.Serialize(response), acDomain.StartTime);
                else
                    LogHelper.Error("请求处理结束", JsonHelper.Serialize(response), acDomain.StartTime);
                context = null;
                response = null;
                appInfo = null;
                request = null;
            }
            return response;
        }
         * 
         */


    } 
}
