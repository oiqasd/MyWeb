using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Biz
{
    public class DataBaseEnum
    {
        /// <summary>
        /// 新闻列表是否启用
        /// </summary>
        public enum EN_ArticleTypeUsed
        {
            /// <summary>
            /// 不启用
            /// </summary>
            Disable = 0,
            /// <summary>
            /// 启用
            /// </summary>
            Enabled = 1
        }

        /// <summary>
        /// 评论
        /// 0:需审核；1:审核通过；2:审核失败
        /// </summary>
        public enum CommentState
        {
            
            /// <summary>
            /// 需审核
            /// </summary>
            Audit = 0,
            /// <summary>
            /// 审核通过
            /// </summary>
            Pass = 1,
            /// <summary>
            /// 审核失败
            /// </summary>
            Failure=2
        }

        /// <summary>
        /// 新闻类别列表
        /// </summary>
        public enum em_articleTypeList
        {
            /// <summary>
            /// 食品
            /// </summary>
            FOOD = 2,
            /// <summary>
            /// 其他
            /// </summary>
            OTHER = 3
        }
    }
}
