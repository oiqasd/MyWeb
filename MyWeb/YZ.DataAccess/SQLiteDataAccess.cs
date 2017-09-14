using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Service.SQLite
{
    public class SQLiteDataAccess
    {
        string constr = "Data Source=D:/WebSite/MyWeb/Data/DBase.db;Pooling=true;FailIfMissing=false";

        #region Instance
        private static SQLiteDataAccess _Instance;
        private static object _asynObject = new object();
        public static SQLiteDataAccess Instance()
        {
            if (_Instance == null)
                lock (_asynObject)
                    if (_Instance == null)
                        _Instance = new SQLiteDataAccess();
            return _Instance;
        }
        private SQLiteDataAccess()
        {
        }
        #endregion

        public DataSet getdata()
        {
            using (SQLiteConnection con = new SQLiteConnection(constr))
            {
                if (con.State != ConnectionState.Open) con.Open();

                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = "select * from h_user";
                cmd.CommandType = CommandType.Text;

                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
        }

        public bool insert(userinfo user)
        {
            using (SQLiteConnection con = new SQLiteConnection(constr))
            {
                if (con.State != ConnectionState.Open) con.Open();

                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = string.Format("INSERT into h_user_20161024(nickname ,realname ,email,regmobile,mobile ,password ,sheng ,shi,xian,jiedao)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                                                     user.nickname, user.realname, user.email, user.regmobile, user.mobile, user.password, user.sheng, user.shi, user.xian, user.jiedao);
                cmd.CommandType = CommandType.Text;

                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }

    public class userinfo
    {
        public string nickname { get; set; }
        public string realname { get; set; }
        public string email { get; set; }
        public string regmobile { get; set; }
        public string mobile { get; set; }
        public string password { get; set; }
        public string sheng { get; set; }
        public string shi { get; set; }
        public string xian { get; set; }
        public string jiedao { get; set; }
    }
}
