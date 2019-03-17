using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperLib.Settings
{
    public class MapSettings
    {
        /// <summary>
        /// The collection will not participate in the mapping
        /// </summary>
        public bool SkipCollections { get; set; }

        /// <summary>
        /// Only registered .net types
        /// </summary>
        public bool SkipCustomTypes { get; set; }
    }
}
