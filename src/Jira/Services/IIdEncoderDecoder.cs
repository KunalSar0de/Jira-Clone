using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.Services
{
    public interface IIdEncoderDecoder
    {
        string EncodeId(int id); 
        int DecodeId(string id); 

    }
}