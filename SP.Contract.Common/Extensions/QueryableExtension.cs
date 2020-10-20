using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SP.Contract.Common.Extensions
{
    public static class QueryableExtension
    {
        private static readonly MethodInfo OrderByMethod =
            typeof(Queryable).GetMethods().Single(method => method.Name == "OrderBy" && method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod =
            typeof(Queryable).GetMethods().Single(method => method.Name == "OrderByDescending" && method.GetParameters().Length == 2);

        private static readonly MethodInfo GroupByMethod =
            typeof(Queryable).GetMethods().Single(method => method.Name == "GroupBy" && method.GetParameters().Length == 2);

        public static bool PropertyExists<T>(this IQueryable<T> source, string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                                                       BindingFlags.Public | BindingFlags.Instance) != null;
        }

        public static IQueryable<T> OrderByProperty<T>(
            this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                                                    BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }

            var parameterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(parameterExpression, propertyName);
            var lambda = Expression.Lambda(orderByProperty, parameterExpression);
            var genericMethod =
                OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            var ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }

        public static IQueryable<T> OrderByPropertyDescending<T>(
            this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                                                    BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }

            var parameterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(parameterExpression, propertyName);
            var lambda = Expression.Lambda(orderByProperty, parameterExpression);
            var genericMethod =
                OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            var ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }
    }
}
