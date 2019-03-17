using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleMapperLib.Extensions;
using SimpleMapperLib.Settings;

namespace SimpleMapperLib.Mapper
{
    public static class SimpleMapper
    {
        /// <summary>
        /// Native object mapping
        /// </summary>
        /// <typeparam name="TDest">Destination object</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="settings">Mapping settings</param>
        /// <returns></returns>
        public static TDest MapTo<TDest>(this object source, MapSettings settings = null)
        {
            var sourceProperties = source.GetType().GetProperties().ApplySettings(settings);
            var instance = (TDest)Activator.CreateInstance(typeof(TDest));
            foreach (var propertyInfo in sourceProperties.Where(x => x.CanRead && !x.GetMethod.IsPrivate))
            {
                instance.GetType().GetProperties().Where(x => !x.SetMethod.IsPrivate)
                    .FirstOrDefault(x => x.Name == propertyInfo.Name && x.PropertyType == propertyInfo.PropertyType && x.CanWrite)?
                    .SetValue(instance, propertyInfo.GetValue(source));
            }
            
            return instance;
        }

        /// <summary>
        /// Native object mapping
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <param name="source">Source object</param>
        /// <param name="secondaryMapping">Action of additional mapping conditions</param>
        /// <param name="settings">Mapping settings</param>
        /// <returns></returns>
        public static TDest MapTo<TDest>(this object source, Action<TDest> secondaryMapping, MapSettings settings = null)
        {
            var destObj = source.MapTo<TDest>(settings);
            secondaryMapping(destObj);
            return destObj;
        }

        /// <summary>
        /// Native each object mapping
        /// </summary>
        /// <typeparam name="TDest">Destination object</typeparam>
        /// <param name="sourceArray">Source object array</param>
        /// <param name="settings">Mapping settings</param>
        /// <returns>Array of destination objects</returns>
        public static IEnumerable<TDest> MapEachTo<TDest>(this IEnumerable<object> sourceArray, MapSettings settings = null)
        {
            var destArray = new List<TDest>();
            foreach (var source in sourceArray)
                destArray.Add(source.MapTo<TDest>(settings));

            return destArray;
        }

        /// <summary>
        /// Native each object mapping
        /// </summary>
        /// <typeparam name="TDest">Destination object</typeparam>
        /// <param name="sourceArray">Source object array</param>
        /// <param name="secondaryMapping">Action of additional mapping conditions</param>
        /// <param name="settings">Mapping settings</param>
        /// <returns>Array of destination objects</returns>
        public static IEnumerable<TDest> MapEachTo<TDest>(this IEnumerable<object> sourceArray, Action<TDest> secondaryMapping, MapSettings settings = null)
        {
            var destArray = new List<TDest>();
            foreach (var source in sourceArray)
                destArray.Add(source.MapTo(secondaryMapping, settings));

            return destArray;
        }
    }
}
