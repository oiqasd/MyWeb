using System.Linq;


namespace YZ.Common
{
    public static class PageLinqExtensions
    {
        public static PageList<T> ToPageList<T>(this IQueryable<T> allItems, int? pageIndex, int pageSize)
        {
            return ToPageList<T>(allItems, pageIndex, pageSize, string.Empty);
        }

        public static PageList<T> ToPageList<T>(this IQueryable<T> allItems, int? pageIndex, int pageSize, string identityColumnName)
        {
            return ToPageList<T>(allItems, pageIndex, pageSize, string.Empty, string.Empty);
        }

        public static PageList<T> ToPageList<T>(this IQueryable<T> allItems, int? pageIndex, int pageSize, string identityColumnName, string sort)
        {
            var truePageIndex = pageIndex ?? 0;
            var itemIndex = truePageIndex * pageSize;
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
            var totalItemCount = allItems.Count();
            return new PageList<T>(pageOfItems, truePageIndex, pageSize, totalItemCount, identityColumnName, sort);
        }
    }
}
