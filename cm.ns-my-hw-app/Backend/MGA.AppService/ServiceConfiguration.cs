using MGA.AppService.Interfaces;
using MGA.AppService.Services;
using MGA.CrossCutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGA.AppService
{
    public class ServiceConfiguration : IServiceConfiguration
    {
		public void InitializeContainer(IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IUserAppService, UserAppService>();
		}
	}
}
