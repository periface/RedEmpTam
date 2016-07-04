using System;
using System.Linq;
using System.Linq.Expressions;

namespace Helpers.ReflectionHelpers
{
    /// <summary>
    /// Hasta esta parte del codigo me queria suicidar señores!!
    /// Sauce: http://stackoverflow.com/questions/18743976/get-iqueryablet-where-any-field-of-t-contains-a-given-string
    /// Sauce: http://reflection241.blogspot.mx/2015/07/c-dynamically-create-lambda-search-on.html
    /// </summary>
    public static class QueryableHelper
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)

        {

            return (IQueryable<T>)OrderBy((IQueryable)source, propertyName);

        }
        public static IQueryable OrderBy(this IQueryable source, string propertyName)

        {

            var x = Expression.Parameter(source.ElementType, "x");

            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);

            return source.Provider.CreateQuery(

                Expression.Call(typeof(Queryable), "OrderBy", new[] { source.ElementType, selector.Body.Type },

                     source.Expression, selector
                     ));
        }
        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)

        {

            return (IQueryable<T>)OrderByDescending((IQueryable)source, propertyName);

        }
        public static IQueryable OrderByDescending(this IQueryable source, string propertyName)

        {

            var x = Expression.Parameter(source.ElementType, "x");

            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);

            return source.Provider.CreateQuery(

                Expression.Call(typeof(Queryable), "OrderByDescending", new[] { source.ElementType, selector.Body.Type },

                     source.Expression, selector

                     ));

        }
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string propertyName, string valueToSearch)
        {

            //var parameterExpression = Expression.Parameter(typeof(T));
            //var propertyInfo = typeof(T).GetProperty(propertyName);
            //var member = Expression.MakeMemberAccess(parameterExpression,propertyInfo);
            //Expression method = Expression.Call(member, "ToLower", null, null);
            //var lambda = Expression.Lambda<Func<T, bool>>
            //    (Expression.Equal(method, Expression.Constant(valueToSearch.ToLower())), parameterExpression);
            //return source.Where(lambda);
            var lambda = SearchAllFieldsWithPropertyDefined<T>(valueToSearch, propertyName);
            return source.Where(lambda);
        }
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string valueToSearch)
        {

            var lambda = SearchAllFields<T>(valueToSearch);
            return source.Where(lambda);
        }

        public static Expression<Func<T, bool>> SearchAllFields<T>(string searchText)
        {
            var t = Expression.Parameter(typeof(T));
            Expression body = Expression.Constant(false);

            var containsMethod = typeof(string).GetMethod("Equals"
                , new[] { typeof(string) });
            var toStringMethod = typeof(object).GetMethod("ToString");

            var stringProperties = typeof(T).GetProperties()
                .Where(property => property.PropertyType == typeof(string));

            foreach (var property in stringProperties)
            {
                var stringValue = Expression.Call(Expression.Property(t, property.Name),
                    toStringMethod);
                var nextExpression = Expression.Call(stringValue,
                    containsMethod,
                    Expression.Constant(searchText));

                body = Expression.OrElse(body, nextExpression);
            }

            return Expression.Lambda<Func<T, bool>>(body, t);
        }
        public static Expression<Func<T, bool>> SearchAllFieldsWithPropertyDefined<T>(string searchText, string property)
        {
            var t = Expression.Parameter(typeof(T));
            Expression body = Expression.Constant(false);

            var containsMethod = typeof(string).GetMethod("Contains"
                , new[] { typeof(string) });
            var toStringMethod = typeof(object).GetMethod("ToString");

            var stringProperties = typeof(T).GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.Name == property);

            foreach (var propertyValue in stringProperties)
            {
                var stringValue = Expression.Call(Expression.Property(t, propertyValue.Name),
                    toStringMethod);
                var nextExpression = Expression.Call(stringValue,
                    containsMethod,
                    Expression.Constant(searchText));

                body = Expression.OrElse(body, nextExpression);
            }

            return Expression.Lambda<Func<T, bool>>(body, t);
        }
        //public static Expression<Func<T, bool>> SearchAllFieldsWithPropertyDefined<T>(string searchText, string[] properties)
        //{
        //    var t = Expression.Parameter(typeof(T));
        //    Expression body = Expression.Constant(false);

        //    var containsMethod = typeof(string).GetMethod("Contains"
        //        , new[] { typeof(string) });
        //    var toStringMethod = typeof(object).GetMethod("ToString");
        //    var stringProperties = typeof(T).GetProperties()
        //        // ReSharper disable once AccessToModifiedClosure
        //        .Where(property => property.PropertyType == typeof(string) && property.Name == properties.FirstOrDefault(a => a == property.Name)).ToList();

        //    foreach (var property in stringProperties)
        //    {
        //        var element = properties.FirstOrDefault();
        //        var stringValue = Expression.Call(Expression.Property(t, property.Name),
        //            toStringMethod);
        //        var nextExpression = Expression.Call(stringValue,
        //            containsMethod,
        //            Expression.Constant(searchText.ToLower()));

        //        body = Expression.Or(body, nextExpression);
        //        var list = properties.ToList();
        //        list.Remove(element);
        //        properties = list.ToArray();
        //    }

        //    return Expression.Lambda<Func<T, bool>>(body, t);
        //}
    }
}
