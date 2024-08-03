using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Model.DTO
{
    public class ResponseModel
    {
        public bool? IsSuccessful { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }

    }
}
