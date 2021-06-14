using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anastasia_SeniorProject.Domain
{
    public class myAuthResult
    {
        public string Token { get; set; }
        public IEnumerable <string> myError { get; set; }
        public string myUserId { get; set; }
        public bool Successful { get; set; }

    }
}
