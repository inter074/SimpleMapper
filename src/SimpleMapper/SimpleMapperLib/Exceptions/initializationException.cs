using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperLib.Exceptions
{
    public class InitializationException : Exception
    {
        public InitializationException(string message) : base(message) { }
    }
}
