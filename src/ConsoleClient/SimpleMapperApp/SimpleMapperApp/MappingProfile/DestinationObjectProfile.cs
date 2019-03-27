using SimpleMapperApp.Objects;
using SimpleMapperLib.Abstractions;

namespace SimpleMapperApp.MappingProfile
{
    public class DestinationObjectProfile : AbstractCustomMapping
    {
        public DestinationObjectProfile()
        {
            CreateRuleForCustomMap<SourceObject, DestinationObject>()
                .Map(x => x.Count, x => x.Count);
        }

    }
}
