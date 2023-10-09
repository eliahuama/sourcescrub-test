using AutoMapper;
using SourceScrub.Entities;

namespace SourceScrub.Business
{
    public static class MappingConfiguration
    {
        public static void RegisterMappings(IMapperConfigurationExpression config)
        {
            Data.MappingConfiguration.RegisterMappings(config);
        }
    }
}