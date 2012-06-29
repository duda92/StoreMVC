using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Store.Domain.Concrete
{
    public static class TrimExtender
    {
        public static IEnumerable<TEntity> Trim<TEntity>(this IEnumerable<TEntity> collection)
        {
            Type type = typeof(TEntity);

            IEnumerable<PropertyDescriptor> properties = TypeDescriptor.GetProperties(type).Cast<PropertyDescriptor>()
                .Where(p => p.PropertyType == typeof(string));

            //foreach (TEntity entity in collection)
            //{
            //    foreach (PropertyDescriptor property in properties)
            //    {
            //        string value = (string)property.GetValue(entity);

            //        if (!String.IsNullOrEmpty(value))
            //        {
            //            value = value.TrimEnd();
            //            property.SetValue(entity, value);
            //        }
            //    }
            //}

            return collection;
        }

    }
}
