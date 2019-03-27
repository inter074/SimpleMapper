using System.Collections.Generic;
using System.Linq;
using SimpleMapperLib.Abstractions;
using SimpleMapperLib.Exceptions;

namespace SimpleMapperLib.CustomMap
{
    internal class CustomMappingProfiles
    {
        private static CustomMappingProfiles _instance;
        public IEnumerable<AbstractCustomMapping> CustomMappingRules;

        private CustomMappingProfiles() { }

        private CustomMappingProfiles(IEnumerable<AbstractCustomMapping> customMappingRules)
        {
            CustomMappingRules = customMappingRules;
        }

        public static void Initialize(params AbstractCustomMapping[] customMappings)
        {
            if (_instance?.CustomMappingRules?.Any() ?? false)
                throw new InitializationException("Attempt to re-initialize");

            _instance = new CustomMappingProfiles(customMappings);
        }

        public static IEnumerable<AbstractCustomMapping> GetCustomMappingRules()
        {
            if (_instance != null)
                return _instance.CustomMappingRules;
            return new List<AbstractCustomMapping>();
        }
    }
}
