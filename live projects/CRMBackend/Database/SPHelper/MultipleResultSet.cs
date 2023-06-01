using CRMBackend.Utility;
using System.Collections;
using System.Reflection;

namespace CRMBackend.Database.SPHelper
{
    public abstract class MultipleResultSet
    {
        public Type GetInnerType(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();
        }

        public IList CreateListType(PropertyInfo propertyInfo)
        {
            Type? innerType = propertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();

            if (innerType != null)
            {
                return innerType.CreateListOfType();
            }
            else
            {
                return null;
            }
        }
    }
}
