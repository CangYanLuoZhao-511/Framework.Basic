// ReSharper disable All

using System;
using System.Linq;
using System.Linq.Expressions;

namespace CangYanLuoZhao.Framework.ToolBox.Basic.Extensions
{
    /// <summary>
    /// 查询对象扩展
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 构建分页查询
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="current"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IQueryable<TSource> ToPage<TSource>(this IQueryable<TSource> source, int current, int size)
        {
            return source.Skip((current - 1) * size).Take(size);
        }

        /// <summary>
        /// 根据条件选择是否拼接查询对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="hasCondition">是否拼接条件</param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool hasCondition,
            Expression<Func<TSource, bool>> predicate)
        {
            return hasCondition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 条件成立返回第一个委托，否则返回第二个委托
        /// 案例： var users = dbContext.Users.WhereIf(user => user.Age > 18, 
        /// u => u.OrderBy(x => x.Name), 
        /// u => u.OrderByDescending(x => x.Name));
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="condition"></param>
        /// <param name="trueClause"></param>
        /// <param name="falseClause"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable,
            bool condition,
            Func<IQueryable<T>, IQueryable<T>> trueClause,
            Func<IQueryable<T>, IQueryable<T>> falseClause)
        {
            return condition ? trueClause(queryable) : falseClause(queryable);
        }
    }
}