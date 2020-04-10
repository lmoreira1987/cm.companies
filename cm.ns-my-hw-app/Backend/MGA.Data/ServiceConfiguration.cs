using MGA.CrossCutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MGA.Domain.Interfaces.Repository;
using MGA.Data.Repository;

namespace MGA.Data
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        public void InitializeContainer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
		}
    }
}
