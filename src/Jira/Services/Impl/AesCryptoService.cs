using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Jira.Services.Impl
{
    public class AesCryptoService : IAesCryptoService
    {
        private readonly IConfiguration _configuration;

        public AesCryptoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Encrypt(string plainTextPassword, string salt,string userId)
        {
            var finalSalt = _configuration["Keys:PassSecret"].Replace("{#}", userId + "-" + salt);


            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainTextPassword);
            byte[] saltBytes = Encoding.UTF8.GetBytes(finalSalt);
            saltBytes = SHA256.Create().ComputeHash(saltBytes);


            using (MemoryStream ms = new MemoryStream())
            {
                using (Aes myAes = Aes.Create())
                {
                    myAes.KeySize = 256;
                    myAes.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(plainTextPassword, saltBytes);
                    myAes.Key = key.GetBytes(myAes.KeySize / 8);
                    myAes.IV = key.GetBytes(myAes.BlockSize / 8);

                    myAes.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, myAes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}