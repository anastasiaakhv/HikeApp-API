using Anastasia_SeniorProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anastasia_SeniorProject.Services
{
    public interface I_Ident_Service
    {
        Task <myAuthResult> Register_Service_Async(string email, string password);
        Task <myAuthResult> Login_Service_Async(string email, string password);
        
    }
}
