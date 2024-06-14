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
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectCreatedResponse>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom<IdResolver,int>(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom<IdResolver,int>(src => src.CreatedBy));
        }
    }
}