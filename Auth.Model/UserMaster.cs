using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Model
{
    public class UserMaster
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsDisabled { get; set; }
        public string? ProfilePicPath { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public DateTime InsertedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public Guid? InsertedByUserId { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        public DateTime? PasswordUpdatedOn { get; set; }
        public int? PasswordValidDays { get; set; }
        public string? ThirdPartyLoginType { get; set; }
        public string? ThirdPartyLoginUserName { get; set; }
    }
}
