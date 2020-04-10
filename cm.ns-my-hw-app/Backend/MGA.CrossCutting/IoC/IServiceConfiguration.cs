using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGA.CrossCutting.IoC
{
    public interface IServiceConfiguration
    {
        void InitializeContainer(IServiceCollection services, IConfiguration configuration);
    }
}
