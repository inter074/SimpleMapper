using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMapperApp.Objects;
using SimpleMapperLib.Mapper;

namespace SimpleMapperApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new SourceObject(3, "T56C8", "test object", new InternalObject(){Name = "internal"});
            //var dest = source.MapTo<DestinationObject>();
            var list = new List<SourceObject>(){source, new SourceObject(2, "second", "second source obj", new InternalObject())};
            var destList = list.MapEachTo<DestinationObject>();
        }
    }
}
