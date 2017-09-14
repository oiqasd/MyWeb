using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Biz
{
    public abstract class BaseRepository
    {
        protected YYZZEntities _Context;

        public BaseRepository()
        {
            //if (_Context == null)
            //{
            _Context = new YYZZEntities();
            //}
        }

        protected T RunSql<T>(string sql)
        {
            try
            {

                return _Context.Database.SqlQuery<T>(sql).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected List<T> RunSqlGetList<T>(string sql)
        {
            try
            {
                return _Context.Database.SqlQuery<T>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
