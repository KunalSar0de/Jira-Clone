using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using Jira.Request;

namespace Jira.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        private const string pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_` {|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

        public RegisterUserRequestValidator()
        {
            string message = "The password must contain a minimum of 6 characters, including at least one uppercase letter and one special character(@,#,$,!).";


            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
            
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Matches(pattern);

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(ValidatePassword)
                .WithMessage(message);
        }

        private bool ValidatePassword(string password)
        {
            string[] specialChars = {"@","!","#","$"};
            //Length should be greater than 6
            if(password.Length <= 5)
                return false;
            
            if(!specialChars.Any(password.Contains))
                return false;

            return true;


        }
    }
}