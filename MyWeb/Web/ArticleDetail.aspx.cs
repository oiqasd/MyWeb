using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Web.Util;
using YZ.Biz;
using YZ.Biz.Entity;
using YZ.Common.Log;
using YZ.Common.Util; 

namespace YZ.Web.Asp
{
    public partial class ArticleDetail : BasePage
    {
        protected Article _article;
        protected void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);
            try
            {

                if (!IsPostBack)
                {
                    _article = new Article();

                    var action = Request["action"];
                    if (action == "add")
                    {
                        string reMsg;
                        if (string.IsNullOrEmpty(Request["actId"]) || Convert.ToInt32(Request["actId"]) <= 0)
                            reMsg = AddArticle();
                        else
                            reMsg = UpdateArticle(); ;
                        Response.Clear();
                        Response.Write(reMsg);
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                        Response.End();
                    }
                    else if (action == "new")
                    { }
                    else
                    {
                        var aid = Request["aid"];

                        if (!string.IsNullOrEmpty(aid) && Convert.ToInt32(aid) > 0)
                        {
                            GetArticleById(Convert.ToInt32(aid));
                        }
                        else
                        {
                            Response.Redirect("error/NoAccess.html", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Threading.ThreadAbortException){ }
                //System.Threading.Thread.ResetAbort();
                else
                {
                     LogHelper.Fatal("ArticleDetail-Page_Load",ex.Message,ex);
                    Response.Redirect("error/NoAccess.html");
                }
            }
        }

        /// <summary>
        /// 文章类别列表
        /// </summary>
        public string NewsTypeList(int tid)
        {
            HtmlSelect select = (HtmlSelect)this.FindControl("selArticleTypes");
            List<ArticleType> typelist = CacheRepository.Instance.CachedArticleType();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (typelist != null && typelist.Count > 0)
            {
                //ListItem item; 
                foreach (var type in typelist)
                {
                    if (type.id == tid)
                    {
                        sb.Append("<option value='" + type.id + "' selected='selected'>" + type.a_t_ArticleCategory + "</option>");
                    }
                    else sb.Append("<option value='" + type.id + "'>" + type.a_t_ArticleCategory + "</option>");
                    //item = new ListItem(type.id.ToString(), type.a_t_ArticleCategory);
                    //select.Items.Add(item);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <returns></returns>
        private string AddArticle()
        {
            var title = Request["title"];
            var typeId = Request["type"];
            var keyWord = Request["keyWord"];
            var txtForm = Request["txtForm"];
            var isTop = Request["isTop"];
            var content = Request["content"];

            _article = new Article();
            _article.a_Title = title;
            _article.a_TypeId = Convert.ToInt32(typeId);
            _article.a_KeyWord = keyWord;
            _article.a_From = txtForm;
            _article.a_IsTop = Convert.ToBoolean(isTop);
            _article.a_Content = content;
            _article.a_CreateBy = UserID;
            _article.a_CreateDate = DateTime.Now;
            _article.a_ModifiedDate = DateTime.Now;

            //if (ArticleRepository.Instance.AddArticle(_article))
            //return "添加成功";
            //return "添加失败";

            ResponseMsg resMsg = new ResponseMsg();
            if (ArticleRepository.Instance.AddArticle(_article))
            {
                resMsg.IsSuccess = true;
                resMsg.Message = "添加成功";
            }
            else resMsg.Message = "添加失败";

            return JsonHelper.Serialize(resMsg);
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <returns></returns>
        private string UpdateArticle()
        {
            var title = Request["title"];
            var typeId = Request["type"];
            var keyWord = Request["keyWord"];
            var txtForm = Request["txtForm"];
            var isTop = Request["isTop"];
            var content = Request["content"];

            _article = new Article();
            _article.id = Convert.ToInt32(Request["actId"]);
            _article.a_Title = title;
            _article.a_TypeId = Convert.ToInt32(typeId);
            _article.a_KeyWord = keyWord;
            _article.a_From = txtForm;
            _article.a_IsTop = Convert.ToBoolean(isTop);
            _article.a_Content = content;
            _article.a_ModifiedBy = UserID;
            _article.a_ModifiedDate = DateTime.Now;

            ResponseMsg resMsg = new ResponseMsg();
            if (ArticleRepository.Instance.UpdateArticle(_article))
            {
                resMsg.IsSuccess = true;
                resMsg.Message = "更新成功";
            }
            else resMsg.Message = "更新失败";

            return JsonHelper.Serialize(resMsg);
        }

        /// <summary>
        /// 根据文章Id获取文章详情
        /// </summary>
        /// <param name="aid"></param>
        private void GetArticleById(int aid)
        {
            _article = ArticleRepository.Instance.GetArticleById(aid);
        }
    }
}