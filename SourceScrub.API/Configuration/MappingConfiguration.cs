using AutoMapper;
using SourceScrub.API.Models;
using SourceScrub.Entities;

namespace SourceScrub.API.Configuration
{
    public static class MappingConfiguration
    {
        public static void RegisterMappings(IMapperConfigurationExpression config)
        {
            Business.MappingConfiguration.RegisterMappings(config);

            config.CreateMap<QuestionModel, Question>().ReverseMap();
            config.CreateMap<AnswerModel, Answer>().ReverseMap();
            config.CreateMap<TagModel, Tag>().ReverseMap();
            config.CreateMap<UserModel, User>().ReverseMap();
            config.CreateMap<VoteModel, Vote>().ReverseMap();
        }
    }
}