using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Util;
using YZ.Biz;
using YZ.Biz.Entity;
using YZ.Common;
using YZ.Common.Util;

namespace YZ.Web.Asp.Admins
{
    public partial class ArticleTypeList : BasePage
    {
        protected List<ArticleType> articleType { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            articleType = ArticleRepository.Instance.ArticleType();

            var type = Request["type"];
            if (!string.IsNullOrEmpty(type))
            {
                var name = Request["name"];
                var id = Request["tid"];
                ArticleType _articleType = new ArticleType();
                switch (type)
                {
                    case "add":
                        _articleType.a_t_CreateBy = "m";//UserName;
                        _articleType.a_t_CreateDate = DateTime.Now;
                        _articleType.a_t_Flag = 1;
                        _articleType.a_t_ArticleCategory = name;
                        break;
                    case "ban":
                        _articleType.id = Convert.ToInt32(id);
                        _articleType.a_t_Flag = (byte)Enumerator.ArticleType.Normal;
                        break;
                    case "star":
                        _articleType.id = Convert.ToInt32(id);
                        _articleType.a_t_Flag = (byte)Enumerator.ArticleType.Freeze;
                        break; 
                    case "del":
                        //_articleType.id = Convert.ToInt32(id);
                        break;
                }
                
                ResponseMsg resMsg = new ResponseMsg();
                if (type == "add")
                {

                    if (ArticleRepository.Instance.AddArticleType(_articleType))
                    {
                        resMsg.IsSuccess = true;
                        resMsg.Message = "添加成功";
                    }
                    else resMsg.Message = "添加失败";
                }
                else if (type == "del")
                {
                    if (ArticleRepository.Instance.PhysicsDeleteArticleType(Convert.ToInt32(id)))
                    {
                        resMsg.IsSuccess = true;
                        resMsg.Message = "删除成功";
                    }
                    else resMsg.Message = "删除失败";
                }
                else
                {

                    if (ArticleRepository.Instance.DeleteArticleType(_articleType))
                    {
                        resMsg.IsSuccess = true;
                        resMsg.Message = "设置成功";
                    }
                    else resMsg.Message = "设置失败";
                }

                Response.Clear();
                Response.Write(JsonHelper.Serialize(resMsg));
                Response.End();
            }
        }
    }
}