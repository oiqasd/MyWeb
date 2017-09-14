using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Service.DataAccess.Common
{
    /// <summary>
    /// 封装部分数据库操作功能
    /// 碰到复杂的情况可以直接使用企业类库的数据库操作，或者添加功能
    /// 不必要拘泥于这个类 
    /// </summary>
    public class YzDbInfo : BaseDbInfo
    {
        public static YzDbInfo CreateInstance()
        {
            YzDbInfo instance = new YzDbInfo();
            return instance;
        }

        private YzDbInfo()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            db = factory.Create(DBConfig.Instance.strDBConnection);
            ExecuteResult = new DbResult();

            Procdbcomm = db.DbProviderFactory.CreateCommand();
            listDbParameter = new List<System.Data.Common.DbParameter>();
        }
    }
}
