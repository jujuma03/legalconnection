using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LC.CORE.Extensions
{
    public static class IQueryableExtensions
    {

        public static IQueryable<T> OrderByConditionThenBy<T, TResult>(this IQueryable<T> queryable, string condition, Expression<Func<T, TResult>> keySelector, Expression<Func<T, TResult>> ThenBykeySelector)
        {
            if (keySelector == null) return queryable.AsQueryable();

            if (condition == ConstantHelpers.DATATABLE.SERVER_SIDE.DEFAULT.ORDER_DIRECTION)
            {
                return queryable
                    .OrderByDescending(keySelector)
                    .ThenBy(ThenBykeySelector)
                    .AsQueryable();
            }
            else
            {
                return queryable
                    .OrderBy(keySelector)
                    .ThenBy(ThenBykeySelector)
                    .AsQueryable();
            }
        }
        public static IQueryable<T> OrderByCondition<T, TResult>(this IQueryable<T> queryable, string condition, Expression<Func<T, TResult>> keySelector)
        {
            if (keySelector == null) return queryable.AsQueryable();

            if (condition == ConstantHelpers.DATATABLE.SERVER_SIDE.DEFAULT.ORDER_DIRECTION)
            {
                return queryable
                    .OrderByDescending(keySelector)
                    .AsQueryable();
            }
            else
            {
                return queryable
                    .OrderBy(keySelector)
                    .AsQueryable();
            }
        }

        public static IQueryable<T> OrderByDescendingCondition<T, TResult>(this IQueryable<T> queryable, bool condition, Expression<Func<T, TResult>> keySelector)
        {
            if (condition)
            {
                return queryable
                    .OrderByDescending(keySelector)
                    .AsQueryable();
            }
            else
            {
                return queryable
                    .OrderBy(keySelector)
                    .AsQueryable();
            }
        }

    }
}
