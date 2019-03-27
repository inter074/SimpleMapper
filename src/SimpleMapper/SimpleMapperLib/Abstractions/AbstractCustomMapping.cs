using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleMapperLib.CustomMap;
using SimpleMapperLib.Exceptions;

namespace SimpleMapperLib.Abstractions
{
    public abstract class AbstractCustomMapping
    {
        protected static CustomMapping<TSource, TDest> CreateRuleForCustomMap<TSource, TDest>()
        {
            return new CustomMapping<TSource, TDest>();
        }

        public static TDest ApplyCustomMap<TSource, TDest>(TSource source, TDest dest)
        {
            var mapRules = CustomMapping<TSource, TDest>.GetMapRules();
            if (mapRules == null || !mapRules.Any())
                throw new ObjectMappingException("No mapping rules found");

            //todo add logic for use custom map

            return dest;
        }

        public static object ApplyCustomMap<TSource, TDest, TParam>(TSource source, TDest dest, object rule, TParam type)
        {
            var value = ((CustomMap<TSource, TDest, TParam>)rule).Source;
            return value.Invoke(source);
        }

        protected sealed class CustomMapping<TSource, TDest>
        {
            private static CustomMapping<TSource, TDest> _instance;
            public ICollection<object> MapRules { get; set; } = new List<object>();

            internal CustomMapping() { }

            public static ICollection<object> GetMapRules()
            {
                return _instance.MapRules;
            }

            public CustomMapping<TSource, TDest> Map<TProperty>(Func<TSource, TProperty> from, Func<TDest, TProperty> to)
            {
                if (_instance == null)
                    _instance = new CustomMapping<TSource, TDest>();
                //if (from.ReturnType != to.ReturnType)
                //    throw new ObjectMappingException($"non-comparable object types{Environment.NewLine}Source type:{from.ReturnType}; Destination type: {to.ReturnType}");
                _instance.MapRules.Add(new CustomMap<TSource, TDest, TProperty>().AddMap(from, to));
                return _instance;
            }
        }
    }


}
