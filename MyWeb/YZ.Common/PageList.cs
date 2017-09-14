using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Common
{
    public class PageList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortExpression { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public string IdentityColumnName { get; set; }

        public PageList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount, string identityColumnName, string sortExpression)
        {
            if (totalItemCount != 0)
            {
                this.AddRange(items);
            }
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.SortExpression = sortExpression;
            this.TotalItemCount = totalItemCount;
            this.TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            this.IdentityColumnName = identityColumnName;
        }
    }
}
