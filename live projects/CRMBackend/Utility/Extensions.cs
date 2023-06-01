using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace CRMBackend.Utility
{
    public static class ExtensionMethods
    {
        //private static readonly ILogger logger = Log.ForContext(typeof(ExtensionMethods));

        /// <summary>
        /// Have the property.
        ///  <para>
        ///     Extend to the model of type <see cref="object"/>.
        /// </para>
        ///  <para>
        ///     Gets The <see cref="string"/> propertyName is for checking.
        /// </para>
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>Returns a <see cref="Boolean"/> indecating Success of the operation.</returns>
        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        /// <summary>
        /// Have the property.
        /// <para>
        ///     Extend to the model of type <see cref="object"/>.
        /// </para>
        ///  <para>
        ///     Gets The <see cref="string"/> propertyName is for checking.
        /// </para>
        /// <para>
        ///     Gets The <see cref="PropertyInfo"/> PropertyInfo is for storing property detail.
        /// </para>
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns>Returns a <see cref="Boolean"/> indecating Success of the operation.</returns>
        public static bool HasProperty(this object obj, string propertyName, out PropertyInfo? propertyInfo)
        {
            propertyInfo = null;
            if (HasProperty(obj, propertyName))
            {
                propertyInfo = obj.GetType().GetProperty(propertyName);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates the list of type.
        ///  <para>
        ///     Extend to the model of type <see cref="Type"/>.
        /// </para>
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns a Entity type <see cref="IList"/>.</returns>
        public static IList? CreateListOfType(this Type type)
        {
            Type listType = typeof(List<>).MakeGenericType(type);
            return Activator.CreateInstance(listType) as IList;
        }

        /// <summary>
        /// Gets the property.
        ///  <para>
        ///     Extend to the model of type <see cref="object"/>.
        /// </para>
        ///  <para>
        ///     Gets The <see cref="string"/> propertyName is for checking.
        /// </para>
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>Returns a Entity type <see cref="PropertyInfo"/>.</returns>
        public static PropertyInfo? GetProperty(this object obj, string propertyName)
        {
            PropertyInfo? property = default;

            try
            {
                if (obj.HasProperty(propertyName))
                {
                    property = obj.GetType().GetProperty(propertyName);
                }
            }
            catch (Exception Ex)
            {
                //logger.Error(Ex.Message, Ex);
            }

            return property;
        }

        public static object? GetPropertyValue(this object obj, string propertyName)
        {
            try
            {
                if (obj.HasProperty(propertyName, out PropertyInfo? property) && property != null)
                {
                    return property.GetValue(obj);
                }
            }
            catch (Exception Ex)
            {
                //logger.Error(Ex.Message, Ex);
            }

            return null;
        }

        public static bool GetPropertyValue(this object obj, string propertyName, out object? value)
        {
            value = default;

            try
            {
                if (obj.HasProperty(propertyName, out PropertyInfo? property) && property != null)
                {
                    value = property.GetValue(obj);
                    return true;
                }
            }
            catch (Exception Ex)
            {
                //logger.Error(Ex.Message, Ex);
            }

            return false;
        }

        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            if (obj.HasProperty(propertyName))
            {
                PropertyInfo? property = obj.GetProperty(propertyName);

                property?.SetValue(obj, value);
            }
        }

        public static string GetPropertyName<T>(this Expression<Func<T, object>> property)
        {
            LambdaExpression lambda = property;
            MemberExpression? memberExpression = null;

            if (lambda.Body is UnaryExpression unaryExpression)
            {
                if (unaryExpression != null)
                {
                    memberExpression = (MemberExpression)(unaryExpression.Operand);
                }
            }
            else
            {
                memberExpression = (MemberExpression)(lambda.Body);
            }

            return (memberExpression?.Member as PropertyInfo)?.Name ?? string.Empty;
        }

        public static IQueryable<TEntity> IncludeEntities<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            if (includes.Length > 0)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
