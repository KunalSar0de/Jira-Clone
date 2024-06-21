using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Jira.Exceptions;
using Jira.Models;
using Jira.Repository;
using Jira.Request;
using Jira.Response;
using Jira.ValidationMessage;
using Jira.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Jira.Services.Impl
{
    public class SessionService : ISessionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenService _jwtTokenCreationService;
        private readonly IAesCryptoService _aesCryptoService;
        private readonly IIdEncoderDecoder _idEncoderDecoder;
        private readonly LoginRequestValidator _loginRequestValidator;



        public SessionService(
            IUserRepository userRepository,
            IMapper mapper,
            IConfiguration configuration,
            IJwtTokenService jwtTokenCreationService,
            IAesCryptoService aesCryptoService,
            IIdEncoderDecoder idEncoderDecoder,
            LoginRequestValidator loginRequestValidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _jwtTokenCreationService = jwtTokenCreationService;
            _aesCryptoService = aesCryptoService;
            _idEncoderDecoder = idEncoderDecoder;
            _loginRequestValidator = loginRequestValidator;
            _userRepository = userRepository;
        }

        public UserCreatedResponse HandleCreateUser(RegisterUserRequest registerUserRequest)
        {
            User user = new User(){
                Email = registerUserRequest.Email,
                FullName = registerUserRequest.FirstName + " " + registerUserRequest.LastName,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };
            
            var createdUser = _userRepository.Add(user);
            
            createdUser.SetPasswordSalt();
            string password = _aesCryptoService.Encrypt(registerUserRequest.Password,createdUser.Salt, _idEncoderDecoder.EncodeId(createdUser.Id)); 
            createdUser.SetUserPassword(password);

            _userRepository.Update(user);

            return _mapper.Map<UserCreatedResponse>(user);

        }


        public LoginResponse HandleLoginSession(LoginRequest loginRequest)
        {
            //fetch user 
            var user = _userRepository.GetUserByEmail(loginRequest.Email);
            if (user == null)
                throw new ThrowValidationException(UserValidationMessage.UserNotFound);

            var reqPassword = _aesCryptoService.Encrypt(loginRequest.Password, user.Salt, _idEncoderDecoder.EncodeId(user.Id));
            if(reqPassword != user.Password)
                throw new ThrowValidationException(UserValidationMessage.InvalidPassword);


            var tokenResponse = _jwtTokenCreationService.CreateJwtToken(loginRequest, user);
            var response = _mapper.Map<LoginResponse>(tokenResponse);
            return response;
                     
        }

        public void HandleResetPassword(ResetPasswordRequest resetPasswordRequest, int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new ThrowValidationException(UserValidationMessage.UserNotFound);

            
            var reqPassword = _aesCryptoService.Encrypt(resetPasswordRequest.CurrentPassword, user.Salt, _idEncoderDecoder.EncodeId(user.Id));
            if(reqPassword != user.Password)
                throw new ThrowValidationException(UserValidationMessage.InvalidCurrentPassword);

            var newPass =  _aesCryptoService.Encrypt(resetPasswordRequest.NewPassword, user.Salt, _idEncoderDecoder.EncodeId(user.Id));
            
            user.SetUserPassword(newPass);
            _userRepository.Update(user);

        }
    }
}