using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthAPI.BL
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public string Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
