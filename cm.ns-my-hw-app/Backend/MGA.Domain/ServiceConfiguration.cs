using MGA.CrossCutting.IoC;
using MGA.Domain.Interfaces.Services;
using MGA.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGA.Domain
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        public void InitializeContainer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
		}
	}
}
