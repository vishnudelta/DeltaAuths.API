using Auth.DataHelper;
using Auth.Model;
using Auth.Model.DTO;
using Auth.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Repository
{
    public class UserMasterRepo : IUserMaster
    {
        private readonly AppDbConnection _dbConnection;
        private readonly IConfiguration _configuration;

        public UserMasterRepo(AppDbConnection dbConnection, IConfiguration configuration)
        {
            _dbConnection = dbConnection;
            _configuration = configuration;
        }
        public async Task<ResponseModel> AddUserAsync(UserMaster user)
        {
            try
            {
                if(user != null)
                {
                    using (var connection = _dbConnection.CreateConnection())
                    {
                        const string query = @"
                            INSERT INTO [DeltaAuths].[dbo].[Users] (
                                [UserId], [FirstName], [LastName], [UserName], [Password], [IsAdmin], [IsDisabled], 
                                [ProfilePicPath], [EmailId], [MobileNo], [InsertedOn], [LastUpdatedOn], 
                                [InsertedByUserId], [LastUpdatedByUserId], [PasswordUpdatedOn], [PasswordValidDays], 
                                [ThirdPartyLoginType], [ThirdPartyLoginUserName]
                            ) VALUES (
                                @UserId, @FirstName, @LastName, @UserName, @Password, @IsAdmin, @IsDisabled, 
                                @ProfilePicPath, @EmailId, @MobileNo, @InsertedOn, @LastUpdatedOn, 
                                @InsertedByUserId, @LastUpdatedByUserId, @PasswordUpdatedOn, @PasswordValidDays, 
                                @ThirdPartyLoginType, @ThirdPartyLoginUserName
                            );";

                        var parameters = new
                        {
                            UserId = Guid.NewGuid(),
                            user.FirstName,
                            user.LastName,
                            user.UserName,
                            user.Password,
                            user.IsAdmin,
                            user.IsDisabled,
                            user.ProfilePicPath,
                            user.EmailId,
                            user.MobileNo,
                            InsertedOn = DateTime.UtcNow,
                            LastUpdatedOn = DateTime.UtcNow,
                            user.InsertedByUserId,
                            user.LastUpdatedByUserId,
                            user.PasswordUpdatedOn,
                            user.PasswordValidDays,
                            user.ThirdPartyLoginType,
                            user.ThirdPartyLoginUserName
                        };

                        var result = await connection.ExecuteAsync(query, parameters);

                        return new ResponseModel
                        {
                            IsSuccessful = result > 0,
                            Message = result > 0 ? "User added successfully." : "Failed to add user.",
                            Data = result
                        };
                    }
                }
                else
                {
                    return new ResponseModel
                    {
                        IsSuccessful = false,
                        Message = "Failed to add user.",
                        Data = "No data found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Exception occurred.",
                    Data = ex.Message
                };
            }
        }

        public async Task<ResponseModel> UpdateUserAsync(UserMaster user)
        {
            try
            {
                if (user != null)
                {
                    using (var connection = _dbConnection.CreateConnection())
                    {                   
                        const string query = @"
                            UPDATE [DeltaAuths].[dbo].[Users]
                            SET 
                                [FirstName] = @FirstName,
                                [LastName] = @LastName,
                                [UserName] = @UserName,
                                [Password] = @Password,
                                [IsAdmin] = @IsAdmin,
                                [IsDisabled] = @IsDisabled,
                                [ProfilePicPath] = @ProfilePicPath,
                                [EmailId] = @EmailId,
                                [MobileNo] = @MobileNo,
                                [LastUpdatedOn] = @LastUpdatedOn,
                                [LastUpdatedByUserId] = @LastUpdatedByUserId,
                                [PasswordUpdatedOn] = @PasswordUpdatedOn,
                                [PasswordValidDays] = @PasswordValidDays,
                                [ThirdPartyLoginType] = @ThirdPartyLoginType,
                                [ThirdPartyLoginUserName] = @ThirdPartyLoginUserName
                            WHERE [UserId] = @UserId;";

                        var parameters = new
                        {
                            user.UserId,
                            user.FirstName,
                            user.LastName,
                            user.UserName,
                            user.Password,
                            user.IsAdmin,
                            user.IsDisabled,
                            user.ProfilePicPath,
                            user.EmailId,
                            user.MobileNo,
                            LastUpdatedOn = DateTime.UtcNow,
                            user.LastUpdatedByUserId,
                            user.PasswordUpdatedOn,
                            user.PasswordValidDays,
                            user.ThirdPartyLoginType,
                            user.ThirdPartyLoginUserName
                        };

                        var result = await connection.ExecuteAsync(query, parameters);

                        return new ResponseModel
                        {
                            IsSuccessful = result > 0,
                            Message = result > 0 ? "User updated successfully." : "Failed to update user.",
                            Data = result
                        };
                    }
                }
                else
                {
                    return new ResponseModel
                    {
                        IsSuccessful = false,
                        Message = "No user found.",
                        Data = "No user found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Exception occurred.",
                    Data = ex.Message
                };
            }
        }

        public async Task<ResponseModel> DeleteUserAsync(Guid userId)
        {
            try
            {
                using (var connection = _dbConnection.CreateConnection())
                {           
                    const string query = @"
                        DELETE FROM [DeltaAuths].[dbo].[Users]
                        WHERE [UserId] = @UserId;";

                    var parameters = new { UserId = userId };

                    var result = await connection.ExecuteAsync(query, parameters);

                    return new ResponseModel
                    {
                        IsSuccessful = result > 0,
                        Message = result > 0 ? "User deleted successfully." : "Failed to delete user.",
                        Data = result
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Exception occurred.",
                    Data = ex.Message
                };
            }
        }
    }
}
