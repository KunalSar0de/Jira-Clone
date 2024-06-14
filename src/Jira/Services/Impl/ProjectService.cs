using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Jira.Models;
using Jira.Repository;
using Jira.Request;
using Jira.Response;

namespace Jira.Services.Impl
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public ProjectCreatedResponse HandleCreateProject(CreateProjectRequest createProjectRequest, int userId)
        {
            var project = new Project
            {
                Name = createProjectRequest.Name,
                Key = GenerateProjectKey(createProjectRequest.Name).ToUpper(),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                CreatedBy = userId,
                IsActive = true
            };

            var projectResponse = _projectRepository.Add(project);

            return _mapper.Map<ProjectCreatedResponse>(projectResponse);

        }

        private string GenerateProjectKey(string name)
        {
            StringBuilder result = new StringBuilder();
            int count = 0;

            foreach (char c in name)
            {
                if (char.IsUpper(c))
                {
                    result.Append(c);
                    count++;
                    if (count == 3)
                    {
                        break;
                    }
                }
            }

            if (count < 2)
            {
                for (int i = 0; i < name.Length && result.Length < 2; i++)
                {
                    if (!char.IsUpper(name[i]))
                    {
                        result.Append(name[i]);
                    }
                }
            }

            return result.ToString();
        }
    }
}