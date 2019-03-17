using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperApp.Objects
{
    public class InternalObject
    {
        public InternalObject(){}
        public InternalObject(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{nameof(InternalObject)}.{Name ?? "EmptyName"}";
        }

        public InternalObject SetFullName()
        {
            Name = ToString();
            return this;
        } 
    }
}
