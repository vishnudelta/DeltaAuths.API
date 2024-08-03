using Auth.DataHelper;
using Auth.Model.DTO;
using Auth.Services;
using Azure;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Auth.Repository
{
    public class UserAuthRepo : IUserAuth
    {
        private readonly AppDbConnection _dbConnection;
        private readonly IConfiguration _configuration;

        public UserAuthRepo(AppDbConnection dbConnection, IConfiguration configuration)
        {
            _dbConnection = dbConnection;
            _configuration = configuration;
        }

        private string GenerateJwtToken(string UserName, string Password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ResponseModel> GetUserAsync(string username, string password)
        {
            string Token;
            var query = " SELECT count(*) FROM Users " +
                        " WHERE UserName = @Username " +
                        " AND Password = @Password  " +
                        " AND ISNULL(IsDisabled, 0) = 0 ";

            try
            {
                using (var connection = _dbConnection.CreateConnection())
                {
                    var cntExist = await connection.ExecuteScalarAsync(query, new { Username = username, Password = password }, commandType: System.Data.CommandType.Text);

                    if (Convert.ToInt32(cntExist) > 0)
                    {
                        Token = GenerateJwtToken(username, password);

                        query = " SELECT *, '" + Token + "' as 'Token' FROM Users " +
                                " WHERE UserName = @Username " +
                                " AND Password = @Password  " +
                                " AND ISNULL(IsDisabled, 0) = 0 ";

                        var response = await connection.QueryAsync(query, new { Username = username, Password = password }, commandType: System.Data.CommandType.Text);
                        return new ResponseModel() { Message = "successful", IsSuccessful = true, Data = response };
                    }
                    else
                    {
                        return new ResponseModel() { IsSuccessful = false, Message = "no user found.", Data = "no user found." };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel() { IsSuccessful = false, Message = "exception false", Data = ex.Message };
            }
        }
    }
}
