using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleMapperLib.Settings;

namespace SimpleMapperLib.Extensions
{
    public static class MapperSettingsExtensions
    {
        /// <summary>
        /// Using custom settings for mapping 
        /// </summary>
        /// <param name="propertys">Properties of source object</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static PropertyInfo[] ApplySettings(this PropertyInfo[] propertys, MapSettings settings)
        {
            if (settings != null && propertys != null && propertys.Any())
            {
                if (settings.SkipCollections)
                    propertys = propertys.Where(x => !typeof(IEnumerable<object>).IsAssignableFrom(x.PropertyType))
                        .ToArray();

                if (settings.SkipCustomTypes)
                    propertys = propertys.Where(x => x.PropertyType.FullName?.Contains("System") ?? false)
                        .ToArray();
            }

            return propertys;
        }
    }
}
