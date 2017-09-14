using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Biz
{
    /// <summary>
    /// 评论
    /// </summary>
    public class CommentRepository : BaseRepository
    {
        #region Instance
        private static CommentRepository _instance;
        private static object _syncObject = new object();
        public static CommentRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new CommentRepository();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <returns></returns>
        public bool Add(Comment comment)
        {
            _Context.Comments.Add(comment);
            return _Context.SaveChanges() > 0;
        }

        /// <summary>
        /// 文章评论列表
        /// </summary>
        /// <param name="aid">文章ID</param>
        /// <param name="perid">上一条评论的ID</param>
        /// <param name="size">行数</param>
        /// <returns></returns>
        public List<Comment> CommentList(long aid, long perid, int size)
        {
            if (size == 0) size = int.MaxValue;
            var list = _Context.Comments.Where(m => m.titleID == aid && m.state == (byte)DataBaseEnum.CommentState.Pass &&
                m.isson == false && m.id > perid).OrderBy(m => m.id).Take(size).ToList();
            return list;
        }

        /// <summary>
        /// 文章回复评论列表
        /// </summary>
        /// <param name="aid">文章ID</param>
        /// <param name="pid">评论的ID</param>
        /// <param name="perid">上一条回复的ID</param>
        /// <param name="size">行数</param>
        /// <returns></returns>
        public List<Comment> ReplyCommentList(long aid, long pid, long perid, int size)
        {
            if (size == 0) size = int.MaxValue;
            var list = _Context.Comments.Where(m => m.titleID == aid && m.state == (byte)DataBaseEnum.CommentState.Pass &&
                m.isson == true && m.parentid == pid && m.id > perid).OrderBy(m => m.id).Take(size).ToList();
            return list;
        }

        /// <summary>
        /// 评论点赞
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public bool Praise(long cid)
        {
            var model = _Context.Comments.Where(m => m.id == cid).FirstOrDefault();
            if (model == null || model.id <= 0) return false;
            if (model.cool <= 0) model.cool = 1;
            else model.cool = model.cool + 1;
            return _Context.SaveChanges() > 0;

        }
    }


}
