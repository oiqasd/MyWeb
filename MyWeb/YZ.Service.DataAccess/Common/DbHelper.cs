using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Service.DataAccess.Common
{
    public class DbHelper
    {
        /// <summary>
        /// 对于查询参数里，有些时候查询不需要根据此条件查，则查询语句不拼接
        /// </summary>
        public static void AddParamForQuery(BaseDbInfo dbInfo, StringBuilder sqlSb, string sqlStr, string paramName, DbType type, object value, object defaultValue)
        {
            if (value == null) return;
            ///如果查询值等于默认值则不需要查询此项
            if (defaultValue != null && value.ToString() == defaultValue.ToString())
            {
                return;
            }
            AddParamForQuery(dbInfo, sqlSb, sqlStr, paramName, type, value);
        }
        /// <summary>
        /// 为SQL语句查询添加查询项
        /// </summary>
        /// <param name="dbInfo"></param>
        /// <param name="sqlSb"></param>
        /// <param name="sqlStr"></param>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        public static void AddParamForQuery(BaseDbInfo dbInfo, StringBuilder sqlSb, string sqlStr, string paramName, DbType dbType, object value)
        {
            sqlSb.AppendFormat(sqlStr, paramName);
            DbParameter param = dbInfo.Procdbcomm.CreateParameter();
            param.DbType = dbType;
            param.Value = value;
            param.ParameterName = "@" + paramName;
            dbInfo.ListDbParameter.Add(param);
        }

        /// <summary>
        /// 执行多条查询
        /// </summary>
        /// <param name="db"></param>
        /// <param name="listDbParameter"></param>
        /// <param name="querySqlString"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(BaseDbInfo dbInfo, params string[] querySqlString)
        {
            DataSet ds = new DataSet();
            int i = 0;
            foreach (var item in querySqlString)
            {
                using (DbCommand Procdbcomm = dbInfo.GetSqlStringCommand(item))
                {
                    if (dbInfo.ListDbParameter != null && dbInfo.ListDbParameter.Count > 0)
                    {
                        foreach (var param in dbInfo.ListDbParameter)
                        {
                            var p = Procdbcomm.CreateParameter();
                            p.ParameterName = param.ParameterName;
                            p.DbType = param.DbType;
                            p.Value = param.Value;
                            Procdbcomm.Parameters.Add(p);
                        }
                    }
                    dbInfo.DB.LoadDataSet(Procdbcomm, ds, "table" + i);
                    i++;
                }
            }
            dbInfo.Clear();
            dbInfo = null;
            querySqlString.RemoveWhere(t => !string.IsNullOrEmpty(t));
            querySqlString = null;
            return ds;
        }
    }
}
