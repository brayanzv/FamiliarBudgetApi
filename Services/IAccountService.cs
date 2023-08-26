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
    public interface IAccountService
    {
        public OperationResult<AuthenticationResponse> RegisterAccount(UserCreationDto userCreation);
        public AuthenticationResponse Login(UserCredentials credentials);
        public AuthenticationResponse Refreshtoken();
        public OperationResult<UserResponseDto> UpdateAccount(int id, UserUpdateDto userUpdate);
        public OperationResult<UserResponseDto> ChangePassword(int id, UserPasswordDto passwordDto);
        public OperationResult<UserResponseDto> GetAccount(int id);
    }
}
