using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperApp.Objects
{
    public class DestinationObject
    {
        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public InternalObject InternalObject { get; set; }
    }
}
