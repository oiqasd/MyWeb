using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Service.DataAccess.Common;

namespace YZ.Service.DataAccess
{
    public class ExampleDataAccess
    {
        #region Instance
        private ExampleDataAccess()
        {
        }
        //使用单例
        private static ExampleDataAccess _instance;
        private static object _syncObject = new object();
        public static ExampleDataAccess Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new ExampleDataAccess();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public int Update(long uid)
        {
            YzDbInfo db = YzDbInfo.CreateInstance();
            db.GetSqlStringCommand("UPDATE acc_balance SET IsAutoBid=1 WHERE UserID=@UID");
            db.AddInParameter("@UID", DbType.Int64, uid);
            return db.ExecuteNonQuery();
        }

        public DataSet GetInfo(long uid)
        {
            YzDbInfo dbInfo = YzDbInfo.CreateInstance();
            StringBuilder sqlSB = new StringBuilder();

            sqlSB.Append("SELECT * FROM acc_balance where 1=1 ");
            DbHelper.AddParamForQuery(dbInfo, sqlSB, " and {0} = @{0} ", "UserID", DbType.Int64, uid, -10); 

            DataSet ds = DbHelper.ExecuteDataSet(dbInfo, sqlSB.ToString());
            return ds;
        }
    }
}
