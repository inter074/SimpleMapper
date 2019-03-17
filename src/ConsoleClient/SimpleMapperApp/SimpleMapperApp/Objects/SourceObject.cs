using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperApp.Objects
{
    public class SourceObject
    {
        public SourceObject(int count, string name, string description, InternalObject internalObject)
        {
            Count = count;
            Name = name;
            Description = description;
            InternalObject = internalObject;
            IsActive = true;
        }

        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; private set; }
        public InternalObject InternalObject { get; set; }
    }
}
