using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YZ.Common;

namespace YZ.Biz
{
    public class UserRepository : BaseRepository
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool AddUser(UserInfo m)
        {
            _Context.UserInfoes.Add(m);
            return _Context.SaveChanges() > 0;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public bool UpdateUser(UserInfo u)
        {
            var e = _Context.UserInfoes.FirstOrDefault(m => m.U_Id == u.U_Id);
            if (e == null) return false;

            return _Context.SaveChanges() > 0;

        }

        /// <summary>
        /// 根据用户名、邮箱、手机登录
        /// </summary>
        /// <param name="prm"></param>
        /// <returns></returns>
        public UserInfo GetUserinfoByLogin(string prm)
        {
            var e = _Context.UserInfoes.FirstOrDefault(m => (m.U_UserName == prm || m.U_MobilePhone == prm || m.U_Email == prm) && m.U_State != (byte)Enumerator.UserStates.Delete);

            return e;
        }
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="prm"></param>
        /// <returns></returns>
        public UserInfo GetUserinfoByUserName(string UserName)
        {
            var e = _Context.UserInfoes.FirstOrDefault(m => (m.U_UserName == UserName) && m.U_State != (byte)Enumerator.UserStates.Delete);

            return e;
        }
        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        /// <param name="prm"></param>
        /// <returns></returns>
        public void UpdateUserLoginTime(long uid)
        {
            var e = _Context.UserInfoes.FirstOrDefault(m => m.U_Id == uid);
            if (e != null)
            {
                e.U_LastLoginTime = DateTime.Now;
                _Context.SaveChanges();
            }

        }

        /// <summary>
        /// 验证是否存在邮箱
        /// </summary>
        /// <param name="email"></param>
        /// <returns>存在　true</returns>
        public bool EmailExists(string email)
        {
            var e = _Context.UserInfoes.FirstOrDefault(m => m.U_Email == email);
            if (e != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 验证是否存在用户名
        /// </summary>
        /// <param name="email"></param>
        /// <returns>存在　true</returns>
        public bool UserNameExists(string username)
        {
            var e = _Context.UserInfoes.FirstOrDefault(m => m.U_UserName == username);
            if (e != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 验证是否存在手机号码
        /// </summary>
        /// <param name="email"></param>
        /// <returns>存在　true</returns>
        public bool PhoneNumberExists(string phone)
        {
            var e = _Context.UserInfoes.FirstOrDefault(m => m.U_MobilePhone == phone);
            if (e != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据用户Id获取用户详细信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UserDetail GetUserDetailByUserId(long UserId)
        {
            var e = _Context.UserDetails.FirstOrDefault(m => m.U_Id == UserId);

            return e;
        }
        /// <summary>
        /// 添加用户详情
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool AddUserDetail(UserDetail m)
        {
            var e = _Context.UserDetails.Add(m);

            return UpdateDataBase();
        }
        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool UpdateDataBase()
        {
            return _Context.SaveChanges() > 0;
        }
    }
}
