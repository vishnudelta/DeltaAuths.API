using Auth.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Services
{
    public interface IUserAuth
    {
        Task<ResponseModel> GetUserAsync(string Username, string Password);
    }
}
