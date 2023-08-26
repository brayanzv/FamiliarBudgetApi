using AutoMapper;
using Data.DataAccess;
using Data.DTOs;
using Data.Entity;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDao accountDao;
        private readonly IMapper mapper;
        private readonly TokenGenerator tokenGenerator;
        private readonly HashService hashService;
        private readonly CurrentUser currentUser;

        public AccountService(IAccountDao accountDao,
            IMapper mapper, 
            TokenGenerator tokenGenerator, 
            HashService hashService,
            CurrentUser currentUser
            )
        {
            this.accountDao= accountDao;
            this.mapper = mapper;
            this.tokenGenerator = tokenGenerator;
            this.hashService = hashService;
            this.currentUser = currentUser;
        }

        public OperationResult<UserResponseDto> ChangePassword(int id, UserPasswordDto passwordDto)
        {

            if (id != currentUser.GetCurrentUser().UserId)
            {
                return OperationResult<UserResponseDto>.Failure("You can't change this password");
            }
            var userDB = accountDao.GetAccount(id);

            passwordDto.Password = hashService.Hash(passwordDto.Password);

            mapper.Map(passwordDto, userDB);

            accountDao.ChangePassword();

            var userResponse = mapper.Map<UserResponseDto>(userDB);
            return OperationResult<UserResponseDto>.Success(userResponse);
        }

        public OperationResult<UserResponseDto> GetAccount(int id)
        {
            if (id != currentUser.GetCurrentUser().UserId)
            {
                return OperationResult<UserResponseDto>.Failure("You cannot get this information from this user");
            }

            var user = accountDao.GetAccount(id);

            var userResponse = mapper.Map<UserResponseDto>(user);

            return OperationResult<UserResponseDto>.Success(userResponse);
        }

        public AuthenticationResponse Login(UserCredentials credentials)
        {
            var user = accountDao.GetAccount(credentials.Email);
            if(user == null || hashService.Hash(credentials.Password) != user.Password)
            {
                return null;
            }

            return tokenGenerator.BuildToken(user);
        }

        public AuthenticationResponse Refreshtoken()
        {
            return tokenGenerator.BuildToken(currentUser.GetCurrentUser());
        }

        public OperationResult<AuthenticationResponse> RegisterAccount(UserCreationDto userCreation)
        {
            if (accountDao.GetAccount(userCreation.Email) != null)
            {
                return OperationResult<AuthenticationResponse>.Failure("Please review your credentials.");
            }

            var userEntity = mapper.Map<User>(userCreation);

            userEntity.Password= hashService.Hash(userCreation.Password);

            var userEntityResponse = accountDao.RegisterAccount(userEntity);

            var authenticatedUser = tokenGenerator.BuildToken(userEntityResponse);

            return OperationResult<AuthenticationResponse>.Success(authenticatedUser);
        }

        public OperationResult<UserResponseDto> UpdateAccount(int id, UserUpdateDto userUpdate)
        {
            var userDB = accountDao.GetAccount(id);

            if (currentUser.GetCurrentUser().UserId != id)
            {
                return OperationResult<UserResponseDto>.Failure("User information cannot be changed.");
            }

            var user = mapper.Map(userUpdate, userDB);
            accountDao.UpdateAccount();

            var userResponse = mapper.Map<UserResponseDto>(user);

            return OperationResult<UserResponseDto>.Success(userResponse);
        }
    }
}
