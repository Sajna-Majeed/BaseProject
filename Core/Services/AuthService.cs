using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Security;
using System.Data;

namespace Core.Services
{
 

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly JwtTokenGenerator _jwt;
        private readonly RefreshTokenService _refresh;

        public AuthService(IUnitOfWork uow,
                           JwtTokenGenerator jwt,
                           RefreshTokenService refresh)
        {
            _uow = uow;
            _jwt = jwt;
            _refresh = refresh;
        }

        public async Task<object> LoginAsync(LoginDto dto)
        {
            string sql="Select * from Users where Username=@Username";
            var user = await _uow.Repository.QuerySingleAsync<User>(sql, new { dto.Username },CommandType.Text);

            if (user == null || !PasswordHasher.Verify(dto.Password, user.Password))
                throw new Exception("Invalid credentials");


            var accessToken = _jwt.Generate(user.UserId,user.Username, user.Role);
            var refreshToken = _refresh.Generate();

            await _uow.Repository.ExecuteAsync(
                "sp_SaveRefreshToken",
                new
                {
                    user.UserId,
                    Token = refreshToken,
                    Expiry = _refresh.Expiry()
                });


            return new
            {
                accessToken,
                refreshToken
            };
        }


        public async Task<object> RefreshAsync(string oldToken)
        {
            var token = await _uow.Repository.QuerySingleAsync<RefreshToken>(
                "sp_GetRefreshToken",
                new { Token = oldToken });

            if (token == null || token.IsRevoked || token.ExpiryDate < DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            await _uow.Repository.ExecuteAsync(
                "sp_RevokeRefreshToken",
                new { oldToken });

            var newRefresh = _refresh.Generate();

            await _uow.Repository.ExecuteAsync(
                "sp_SaveRefreshToken",
                new
                {
                    token.UserId,
                    Token = newRefresh,
                    Expiry = _refresh.Expiry()
                });

            var user = await _uow.Repository.QuerySingleAsync<User>(
                "sp_User_GetById",
                new { token.UserId });

            var access = _jwt.Generate(user.UserId,user.Username, user.Role);

            return new { access, refresh = newRefresh };
        }


    }

}
