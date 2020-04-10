using MGA.CrossCutting.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MGA.CrossCutting.Provider.Interface;
using MGA.CrossCutting.Provider;

namespace DWSP.Application.Contracts
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        public void InitializeContainer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
