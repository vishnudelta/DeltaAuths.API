using Auth.Model;
using Auth.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Services
{
    public interface IUserMaster
    {
        Task<ResponseModel> AddUserAsync(UserMaster user);
        Task<ResponseModel> UpdateUserAsync(UserMaster user);
        Task<ResponseModel> DeleteUserAsync(Guid userId);
    }
}
