using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anastasia_SeniorProject.Contracts.V1

{
    public class myRoutes
    {
        public const string myVersion = "v1";
        public const string myRoot = "api";
        public const string myBase = myRoot + "/" + myVersion;
        
        public static class Identity
        {
            public const string Login = myBase + "/" + "identity" + "/" + "login";
            public const string Register = myBase + "/" + "identity" + "/" + "register";
            public const string GetAllUsers = myBase + "/" + "identity" + "/" + "GetAllUsers";
            public const string GetUserByEmail = myBase + "/" + "identity" + "/" + "GetUserByEmail";
            public const string GetUserById = myBase + "/" + "identity" + "/" + "GetUserById";
        }
    }
}

