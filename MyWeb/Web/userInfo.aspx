<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userInfo.aspx.cs" Inherits="YZ.Web.Asp.userInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel='stylesheet' href='/css/bootstrap5152.css?ver=1.0' type='text/css' media='all' />
    <link rel='stylesheet' href='/css/responsive5152.css?ver=1.0' type='text/css' media='all' /> 
    <link rel='stylesheet' href='/css/main5152.css?ver=1.0' type='text/css' media='all' />
    <link rel='stylesheet' href='/css/custom5152.html?ver=1.1' type='text/css' media='all' />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/userInfo.css" rel="stylesheet" />
    <script src="js/jquery-1.8.3.min.js"></script>
    <script src="js/CommonHelper.js"></script>
    <script src="js/PersonalInfo.js?2"></script>
    <script src="js/JsAddress/geo.js"></script>
    <title></title>

</head>
<body>
    <%-- <div class="width_980" style="margin-left: 10%">--%>
    <div>
        <div class="userinfo margin_left">
            <%--float_left --%>
            <%-- <div class="userinfo_one clearfix">
                <div class=" float_left">
                    <img src="images/UserHead.png" class="user_img" />
                    <a href="#" class="modified_img ">修改头像</a>
                </div>
                <p class=" float_left" style="margin-top: 20px;">你好，XXX，欢迎来到本网站！</p>
            </div>--%>
            <div class="userinfo_one wh140 float_left">
                <div class=" float_left">
                    <img src="images/face/df3654.jpg" class="user_img" />
                    <a class="modified_img">修改头像</a>
                </div>
                <%--<p class=" float_left" style="margin-top: 20px;">你好，公主风，欢迎来到本网站！</p>--%>
            </div>

            <div class="userinfo_second float_right">

                <div class="info_nav  clearfix">
                    <p>你好，<span id="spNickName">公主风</span>，欢迎来到本网站！</p>
                    <div class="float_left">
                        <a>兴趣爱好</a>
                        <a>个人资料</a>
                    </div>

                    <div class="float_right set">
                        <a id="ToEdit">修改</a>
                        <a id="ToSave" class="float_left">保存</a>
                        <a id="ToCancel" class=" margin_left_6 float_right">取消</a>
                    </div>
                </div>

                <div id="divShow">

                    <%--<a href="#" style="margin-left: 580px;">[修改]</a>--%>
                    <!--修改-->
                    <div class="wh500 margin_bottom_30">
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">真实姓名:</div>
                            <div class="user_name float_right"><span id="spRealName">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">性别:</div>
                            <div class="user_name float_right"><span id="spSex">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">年龄:</div>
                            <div class="user_name float_right"><span id="spAge">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">生日:</div>
                            <div class="user_name float_right">
                                <span id="spCale" />
                                <span id="spBirth">未填写</span>
                            </div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">星座:</div>
                            <div class="user_name float_right"><span id="spConst">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">血型:</div>
                            <div class="user_name float_right"><span id="spBooldType">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">@Email:</div>
                            <div class="user_name float_right"><span id="spEmail">未填写 </span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">感情状况:</div>
                            <div class="user_name float_right"><span id="spEmotState">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">联系方式:</div>
                            <div class="user_name float_right"><span id="spContactWay">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">文化程度:</div>
                            <div class="user_name float_right"><span id="spEducation">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">毕业院校:</div>
                            <div class="user_name float_right"><span id="spSchool">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">证件类型:</div>
                            <div class="user_name float_right"><span id="spPaperType">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">证件号:</div>
                            <div class="user_name float_right"><span id="spPaperNumber">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">公司:</div>
                            <div class="user_name float_right"><span id="spCompany">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">职业:</div>
                            <div class="user_name float_right"><span id="spWorker">未填写</span></div>
                        </div>

                        <div class="user_show clearfix">
                            <div class="user_type float_left ">籍贯:</div>
                            <div class="user_name float_right"><span id="spHomeTown">未填写</span></div>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type float_left ">现居地:</div>
                            <div class="user_name float_right"><span id="spNowPlace">未填写</span></div>
                        </div>

                        <div class="user_show clearfix">
                            <div class="user_type float_left ">详细地址:</div>
                            <div class="user_name float_right"><span id="spAddress">未填写</span></div>
                        </div>

                        <div class="user_show clearfix">
                            <div class="user_type float_left ">其他想说的:</div>
                            <div class="user_name float_right"><span id="spRemark">未填写</span></div>
                        </div>
                    </div>
                </div>

                <div id="divEdit">
                    <%--<a href="#" style="margin-left: 580px;">[保存]</a>--%>

                    <!--保存-->
                    <div class="wh500 margin_bottom_30">
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">昵称:</div>
                            <input type="text" id="txtNickName" maxlength="20" class="user_input float_right" />
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">真实姓名:</div>
                            <input type="text" id="txtRealName" maxlength="8" class="user_input float_right" />
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">性别:</div>
                            <select id="selSex" class="user_select float_right">
                                <option value="保密">保密</option>
                                <option value="男">男</option>
                                <option value="女">女</option>
                            </select>
                            <%-- <input type="text" class="user_input float_right" />--%>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">年龄:</div>
                            <input type="text" id="txtAge" onkeypress="OnlyNumber()" maxlength="3" class="user_input float_right" onfocus="if(this.value=='未填写'){this.value='';this.style.color='#333'}" onblur="if(this.value==''){this.value='未填写';this.style.color='#333'}"  /> <%--value="账 号：" --%>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left  ">生日:</div>
                            <select id="selCale" class="user_select float_right" style="width: 70px!important; display: none">
                                <option value="阳历">阳历</option>
                                <option value="农历">农历</option>
                            </select>
                            <input type="date" id="datBirth" class="user_input float_right" />
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">星座:</div>
                            <select id="selConst" class="user_select float_right">
                                <option value="白羊座">白羊座[Aries]      [3.21-4.19]</option>
                                <option value="金牛座">金牛座[Taurus]     [4.20-5.20]</option>
                                <option value="双子座">双子座[Gemini]     [5.21-6.21]</option>
                                <option value="巨蟹座">巨蟹座[Cancer]     [6.22-7.22]</option>
                                <option value="狮子座">狮子座[Leo]        [7.23-8.22]</option>
                                <option value="处女座">处女座[Virgo]      [8.23-9.22]</option>
                                <option value="天秤座">天秤座[Libra]      [9.23-10.23]</option>
                                <option value="天蝎座">天蝎座[Scorpius]   [10.24-11.22]</option>
                                <option value="射手座">射手座[Sagittarius][11.23-12.21]</option>
                                <option value="摩羯座">摩羯座[Capricorn]  [12.22-1.19]</option>
                                <option value="水瓶座">水瓶座[Aquarius]   [1.20-2.18]</option>
                                <option value="双鱼座">双鱼座[Pisces]     [2.19-3.20]</option>
                            </select>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">血型:</div>
                            <select id="selBooldType" class="user_select float_right">
                                <option value="A">A型</option>
                                <option value="B">B型</option>
                                <option value="AB">AB型</option>
                                <option value="O">O型</option>
                                <option value="其他">其他型</option>
                            </select>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">@Email:</div>
                            <input type="text" id="txtEmail" maxlength="25" class="user_input float_right" />
                        </div>

                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">感情状况:</div>
                            <select id="selEmotState" class="user_select float_right">
                                <option value="嘿嘿" selected="selected">嘿嘿</option>
                                <option value="单身">单身</option>
                                <option value="热恋">热恋</option>
                                <option value="已婚">已婚</option>
                                <option value="离婚">离婚</option>
                                <option value="再婚">再婚</option>
                            </select>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">联系方式:</div>
                            <input type="text" id="txtContactWay" onkeypress="OnlyNumber()" maxlength="20" class="user_input float_right" />
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">文化程度:</div>
                            <select id="selEducation" class="user_select float_right">
                                <option value="小学及以下">小学及以下</option>
                                <option value="初中">初中</option>
                                <option value="高中">高中</option>
                                <option value="中专">中专</option>
                                <option value="大专|本科">大专|本科</option>
                                <option value="研究生">研究生</option>
                                <option value="博士及以上">博士及以上</option>
                            </select>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">毕业院校:</div>
                            <input type="text" id="txtSchool" maxlength="20" class="user_input float_right"onfocus="if(this.value=='未填写'){this.value='';this.style.color='#333'}" onblur="if(this.value==''){this.value='未填写';this.style.color='#333'}" />
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">证件类型:</div>
                            <select id="selPaperType" class="user_select float_right">
                                <option value="身份证" selected="selected">身份证</option>
                                <option value="学生证">学生证</option>
                                <option value="工作证">工作证</option>
                                <option value="士兵证|军官证">士兵证|军官证</option>
                                <option value="护照">护照</option>
                                <option value="其他">其他</option>
                            </select>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">证件号:</div>
                            <input type="text" id="txtPaperNumber" onkeypress="KeyPressAll()" maxlength="20" class="user_input float_right"onfocus="if(this.value=='未填写'){this.value='';this.style.color='#333'}" onblur="if(this.value==''){this.value='未填写';this.style.color='#333'}" />
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">公司:</div>
                            <input type="text" id="txtCompany" maxlength="20" class="user_input float_right"onfocus="if(this.value=='未填写'){this.value='';this.style.color='#333'}" onblur="if(this.value==''){this.value='未填写';this.style.color='#333'}" />
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">职业:</div>
                            <select id="selWorker" class="user_select float_right">
                                <option value="计算机|互联网|通信">计算机|互联网|通信</option>
                                <option value="生产|工艺|制造">生产|工艺|制造</option>
                                <option value="医疗|护理|制药">医疗|护理|制药</option>
                                <option value="金融|银行|投资|保险">金融|银行|投资|保险</option>
                                <option value="商业|服务员|个体经营">商业|服务员|个体经营</option>
                                <option value="文化|广告|传媒">文化|广告|传媒</option>
                                <option value="娱乐|艺术|表演">娱乐|艺术|表演</option>
                                <option value="律师|法务">律师|法务</option>
                                <option value="教育|培训">教育|培训</option>
                                <option value="公务员|行政|事业单位">公务员|行政|事业单位</option>
                                <option value="模特">模特</option>
                                <option value="空姐">空姐</option>
                                <option value="学生">学生</option>
                                <option value="其他">其他</option>
                            </select>
                        </div>

                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">籍贯:</div>
                            <div>
                                <select class="user_select wh82 margin_left_6" name="province" id="s1">
                                    <option></option>
                                </select> 
                                <select class="user_select wh121 margin_left_6 margin_top_6" name="city" id="s2">
                                    <option></option>
                                </select>
                                <br />
                                <select class="user_select margin_left_96 margin_top_6" name="town" id="s3">
                                    <option></option>
                                </select>
                            </div>
                            <%-- <input type="text" id="txtHomeTown" maxlength="20" class="user_input float_right" />--%>
                        </div>
                        <br />
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">现居地:</div>
                            <input type="text" id="txtNowPlace" maxlength="50" class="user_input float_right" onfocus="if(this.value=='未填写'){this.value='';this.style.color='#333'}" onblur="if(this.value==''){this.value='未填写';this.style.color='#333'}"/>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">详细地址:</div>
                            <input type="text" id="txtAddress" maxlength="50" class="user_input float_right" onfocus="if(this.value=='未填写'){this.value='';this.style.color='#333'}" onblur="if(this.value==''){this.value='未填写';this.style.color='#333'}"/>
                        </div>
                        <div class="user_show clearfix">
                            <div class="user_type2 float_left ">其他想说的:</div>
                            <input type="text" id="txtRemark" maxlength="100" class="user_input float_right" onfocus="if(this.value=='未填写'){this.value='';this.style.color='#333'}" onblur="if(this.value==''){this.value='未填写';this.style.color='#333'}" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
