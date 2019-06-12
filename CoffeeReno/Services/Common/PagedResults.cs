using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Services.ViewModels.Request;

namespace Services.Common
{
    public interface IPagedResults<T>
    {
        IList<T> Data { get; set; }
        int Total { get; set; }
    }

    public class PagedResults<T> : IPagedResults<T>
    {
        public IList<T> Data { get; set; }
        public int Total { get; set; }
    }

    public abstract class PaggingHelper
    {
        protected static void OrderBy<T>(ref IQueryable<T> records, PagingRequest page)
        {
            string methodName = page.SortDir == EnumHelper<SortOrder>.GetDisplayValue(SortOrder.Asc) ? "OrderBy" : "OrderByDescending";
            ParameterExpression parameter = Expression.Parameter(records.ElementType, "p");

            MemberExpression memberAccess = Expression.Property(parameter, page.SortField);

            LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);

            MethodCallExpression result = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { records.ElementType, memberAccess.Type },
                records.Expression,
                Expression.Quote(orderByLambda));

            records = records.Provider.CreateQuery<T>(result);
        }

        protected static void Paging<T>(ref IQueryable<T> records, PagingRequest page) where T : class
        {
            if (!page.Skip.HasValue)
                page.Skip = 0;

            if (!page.Take.HasValue)
                page.Take = 10;

            records = records.Skip(page.Skip.Value).Take(page.Take.Value);
        }
    }
}
