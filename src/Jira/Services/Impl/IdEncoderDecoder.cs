using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashidsNet;

namespace Jira.Services.Impl
{
    public class IdEncoderDecoder : IIdEncoderDecoder
    {
        private readonly IHashids _hashids;

        public IdEncoderDecoder(IHashids hashids)
        {
            _hashids = hashids;
        }

        public int DecodeId(string id)
        {
            return _hashids.Decode(id)[0];
        }

        public string EncodeId(int id)
        {
            return _hashids.Encode(id);
        }
    }
}