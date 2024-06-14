using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Jira.AutoMapper.Resolvers;
using Jira.Models;
using Jira.Response;

namespace Jira.AutoMapper
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<User, UserCreatedResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom<IdResolver, int>(src => src.Id));

            CreateMap<JwtTokenCreationResponse,LoginResponse >() 
                .ForMember(dest => dest.Token , opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom<IdResolver, int>(src => src.UserId));   

        }
    }
}