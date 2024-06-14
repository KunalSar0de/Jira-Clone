using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Jira.Repository;
using Jira.Request;

namespace Jira.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        private const string pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_` {|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        public LoginRequestValidator()
        {

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Matches(pattern);


            RuleFor(x => x.Password)
                .NotEmpty();

        }
    }
}