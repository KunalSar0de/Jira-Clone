using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Services;
using System.Security.Cryptography;

namespace Jira.Models
{
    public class User
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public bool IsActive { get; set; }
        public string OTP { get; set; }
        public DateTime OTPCreatedOn { get; set; }
        public int Id { get; set; }
        public Project Project { get; set; }
        public int? ProjectId { get; set; }
        public bool IsProjectAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }


        public void SetUserPassword(string password)
        {
            Password = password;
        }

        public void SetPasswordSalt()
        {
            Salt = GenerateSalt(10);
        }

        
        public void SetProjectAdmin(int projectId)
        {
            IsProjectAdmin = true;
            ProjectId  = projectId;
            ModifiedOn = DateTime.Now;
        }

        private string GenerateSalt(int length)
        {
            byte[] saltBytes = new byte[length];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }
    }
}