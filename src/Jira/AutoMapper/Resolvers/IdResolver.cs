using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Jira.Services;

namespace Jira.AutoMapper.Resolvers
{
    public class IdResolver : IMemberValueResolver<object, object, int, string>
    {
        private readonly IIdEncoderDecoder _idEncoderDecoder;

        public IdResolver(IIdEncoderDecoder idEncoderDecoder)
        {
            _idEncoderDecoder = idEncoderDecoder;
        }

        public string Resolve(object source, object destination, int sourceMember, string destMember, ResolutionContext context)
        {
           return _idEncoderDecoder.EncodeId(sourceMember);
        }
    }
}