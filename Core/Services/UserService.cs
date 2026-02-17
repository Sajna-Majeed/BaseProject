using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Security;
using Microsoft.AspNetCore.Http;
using System.Data;
using static System.Net.WebRequestMethods;

namespace Core.Services
{
  

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUnitOfWork uow,IHttpContextAccessor httpContextAccessor)
        {
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> CreateUserAsync(CreateUserDto dto)
        {
            var hashed = PasswordHasher.Hash(dto.Password);
            var userId = _httpContextAccessor.HttpContext?.Items["UserId"] as int?;

            var result= await _uow.Repository.ExecuteAsync(
                "sp_User_Create",
                new
                {
                    dto.Username,
                    dto.FullName,
                    dto.Email,
                    Mobile=dto.MobileNumber,
                    Password = hashed,
                    Role = dto.Role.ToString(),
                    CreatedBy = userId,
                    CreatedOn=DateTime.UtcNow
                });
            _uow.Commit();  

            return result;
        }
        public async Task UpdateUserAsync(UpdateUserDto dto)
        {
            var userId = _httpContextAccessor.HttpContext?.Items["UserId"] as int?;

            await _uow.Repository.ExecuteAsync(
                "sp_User_Update",
                new
                {
                    dto.Id,
                    dto.Username,
                    dto.FullName,
                    dto.Email,
                    Mobile = dto.MobileNumber,
                    Role =dto.Role.ToString(),
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow
                });

            _uow.Commit();
        }

        public async Task DeleteUserAsync(int id)
        {
            await _uow.Repository.ExecuteAsync(
                "sp_User_Delete",
                new
                {
                    Id = id
                });

            _uow.Commit();
        }


        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _uow.Repository.QueryAsync<User>(
                "sp_User_GetAll");
        }
        public async Task<User> GetUsersByIdAsync(int userId)
        {
            var sql = "Select [FullName] ,[Email],[MobileNumber],[UserName] ,[Password] ,[Role] from Users where userId =  " + userId;
            return await _uow.Repository.QuerySingleAsync<User>( sql,null,CommandType.Text);
        }
    }

}
