using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MSSqlVisual
{
    public class DataAccessHelper
    {
        #region Instance

        private static DataAccessHelper _DataAccess;
        private static object _instance = new object();
        public static DataAccessHelper Instance
        {
            get
            {
                if (_DataAccess == null)
                {
                    lock (_instance)
                    {
                        if (_DataAccess == null)
                            _DataAccess = new DataAccessHelper();
                    }
                }
                return _DataAccess;
            }
        }

        private DataAccessHelper()
        { }
        #endregion

        string strconn = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
        public DataSet GetData(string strsql)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connect = new SqlConnection(strconn))
            {
                connect.Open();
                //using (SqlCommand command = new SqlCommand())
                //{
                //    command.CommandText = strsql;
                //    command.CommandType = CommandType.Text; 
                //}

                using (SqlDataAdapter adapter = new SqlDataAdapter(strsql, connect))
                {
                    adapter.Fill(ds);
                }
            }

            return ds;
        }


        public int Execute(string cmdtext)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(strconn))
                {
                    connect.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connect;
                        command.CommandText = cmdtext;
                        command.CommandType = CommandType.Text;

                        return command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}