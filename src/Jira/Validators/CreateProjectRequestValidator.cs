using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Jira.Repository;
using Jira.Request;
using Jira.Services;

namespace Jira.Validators
{
    public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
    {
        private readonly ITokenHelperService _tokenHelperService;
        private readonly IProjectRepository _projectRepository;

        public CreateProjectRequestValidator(ITokenHelperService tokenHelperService, IProjectRepository projectRepository)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(3)
                .NotEmpty()
                .Must(IsProjectNameAvailable)
                    .WithMessage("The project already exists with a similar name.");


            _tokenHelperService = tokenHelperService;
            _projectRepository = projectRepository;
        }

        private bool IsProjectNameAvailable(string projectName)
        {
            var userId = _tokenHelperService.GetUserDetails().Id;
            return !_projectRepository.IsProjectNameAvailable(projectName, userId);
        }
    }
}