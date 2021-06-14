using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anastasia_SeniorProject.Installer
{
    public interface myInstaller
    {
        void installMyServices(IServiceCollection services, IConfiguration configuration);
    }
}
