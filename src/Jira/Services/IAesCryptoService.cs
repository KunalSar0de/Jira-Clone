using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.Services
{
    public interface IAesCryptoService
    {
        string Encrypt(string plainTextPassword, string salt,string userId);

    }
}