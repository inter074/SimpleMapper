using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperLib.Mapper
{
    public static class SimpleMapper
    {
        /// <summary>
        /// Native object mapping
        /// </summary>
        /// <typeparam name="TDest">Destination object</typeparam>
        /// <param name="source">Source object</param>
        /// <returns></returns>
        public static TDest MapTo<TDest>(this object source)
        {
            var sourceProperties = source.GetType().GetProperties();
            var instance = (TDest)Activator.CreateInstance(typeof(TDest));
            foreach (var propertyInfo in sourceProperties)
            {
                instance.GetType().GetProperties()
                    .FirstOrDefault(x => x.Name == propertyInfo.Name && x.PropertyType == propertyInfo.PropertyType)?
                    .SetValue(instance, propertyInfo.GetValue(source));
            }
            
            return instance;
        }

        public static IEnumerable<TDest> MapEachTo<TDest>(this IEnumerable<object> sourceArray)
        {
            var destArray = new List<TDest>();
            foreach (var source in sourceArray)
                destArray.Add(source.MapTo<TDest>());

            return destArray;
        }
    }
}
