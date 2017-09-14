using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Common
{
    /// <summary>
    /// 所有状态枚举对照表
    /// </summary>
    public class Enumerator
    {
        /// <summary>
        /// 文章类别
        /// </summary>
        public enum ArticleType
        {
            [EnumDescription("正常")]
            Normal = 1,
            [EnumDescription("冻结")]
            Freeze = 0,
        }
        /// <summary>
        /// 文章排序
        /// </summary>
        public enum ArticleOrder
        {
            [EnumDescription("时间")]
            Time = 1,
            [EnumDescription("阅读量")]
            Read = 2,
            [EnumDescription("用户")]
            User = 3,
        }
        /// <summary>
        /// 文章状态
        /// </summary>
        public enum ArticleStates
        {
            [EnumDescription("正常")]
            Normal = 1,
            [EnumDescription("冻结")]
            Freeze = 2,
        }

        /// <summary>
        /// 用户权限
        /// 默认为9(普通权限)
        /// </summary>
        public enum UserAccess
        {
            [EnumDescription("超级管理员")]
            Super = 1,
            [EnumDescription("管理员")]
            Manger = 2,
            [EnumDescription("会员")]
            Member = 9,
        }

        /// <summary>
        /// 用户帐号状态
        /// 默认为1
        /// </summary>
        public enum UserStates
        {
            [EnumDescription("正常")]
            Normal = 1,
            [EnumDescription("冻结")]
            Freeze = 2,
            [EnumDescription("删除")]
            Delete = 9,
        }
    }
}
