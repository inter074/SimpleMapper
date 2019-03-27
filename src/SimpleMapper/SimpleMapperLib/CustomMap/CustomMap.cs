using System;
using System.Linq.Expressions;

namespace SimpleMapperLib.CustomMap
{
    public sealed class CustomMap<TSource, TDest, TProperty>
    {
        public Type TypeOfParametr { get; set; }
        public Func<TSource, TProperty> Source { get; set; }
        public Func<TDest, TProperty> Destination { get; set; }

        public CustomMap<TSource, TDest, TProperty> AddMap(Func<TSource, TProperty> source, Func<TDest, TProperty> destination)
        {
            Source = source;
            Destination = destination;
            TypeOfParametr = typeof(TProperty);
            return this;
        }
    }
}
