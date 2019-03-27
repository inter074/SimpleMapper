using System;

namespace SimpleMapperLib.Exceptions
{
    public class ObjectMappingException : Exception
    {
        public ObjectMappingException(string message) : base(message){}
    }
}
