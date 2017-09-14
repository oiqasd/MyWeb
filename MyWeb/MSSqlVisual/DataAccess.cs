using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;

namespace MSSqlVisual
{
    public class DataAccess
    {

        #region Instance

        private static DataAccess _DataAccess;
        private static object _instance = new object();
        public static DataAccess Instance
        {
            get
            {
                if (_DataAccess == null)
                {
                    lock (_instance)
                    {
                        if (_DataAccess == null)
                            _DataAccess = new DataAccess();
                    }
                }
                return _DataAccess;
            }
        }

        private DataAccess()
        { }
        #endregion

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="type">BASE TABLE、VIEW</param>
        /// <returns></returns>
        public DataSet GetTablesName(string databaseName, string type = "BASE TABLE")
        {

            string strsql = "SELECT TABLE_NAME FROM " + databaseName + ".INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='" + type + "' ORDER BY TABLE_NAME";

            return DataAccessHelper.Instance.GetData(strsql);
        }

        /// <summary>
        /// 获取表里的列名
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public DataSet GetColumnsNames(string tablename)
        {

            string strsql = "Select Name FROM SysColumns Where id=Object_Id('" + tablename + "')";

            return DataAccessHelper.Instance.GetData(strsql);
        }

        public DataSet GetDataByTable(string tbName, int pageSize, int pageIndex, out int totalPage)
        {

            int totalcount = GetAllRows(tbName);
            if (totalcount <= 0)
            {
                totalPage = 0;
                return new DataSet();
            }

            totalPage = (totalcount - 1) / pageSize + 1;

            StringBuilder sb = new StringBuilder();
            sb.Append("select top ")
                .Append(pageSize)
                .Append(" * from (select  * from ")
                .Append(tbName)
                .Append(" EXCEPT select top ")
                .Append(pageSize * pageIndex)
                .Append(" * from ")
                .Append(tbName)
                .Append(" )a");

            return DataAccessHelper.Instance.GetData(sb.ToString());
        }

        /// <summary>
        /// 总行数查询 
        /// </summary>
        /// <param name="tbName"></param>
        /// <returns></returns>
        public int GetAllRows(string tbName)
        {
            string sql = "select count(1) from " + tbName;
            return (int)DataAccessHelper.Instance.GetData(sql).Tables[0].Rows[0][0];
        }


        public int ExcuseSql(string sql)
        {
            return DataAccessHelper.Instance.Execute(sql);
        }
    }
}