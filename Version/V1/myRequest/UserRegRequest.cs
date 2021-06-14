using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anastasia_SeniorProject.Contracts.V1
{
    public class UserRegRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
