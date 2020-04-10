using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MGA.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using MGA.AppService.AutoMapper;
using MGA.CrossCutting.IoC;

namespace MGA.API2
{
    public class Startup
    {
		public Startup(IConfiguration configuration)
		{
			_Configuration = configuration;
		}

		public IConfiguration Configuration { get { return _Configuration; } }
		protected IConfiguration _Configuration;

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			services.AddDbContext<MGAContext>(options => options.UseSqlServer(connectionString));

			services.AddSession();

			services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

			// Add Database Initializer
			services.AddScoped<IDbInitializer, DbInitializer>();

			InitializeContainer(services);

			AutoMapperConfig.RegisterMappings();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//Generate EF Core Seed Data
			dbInitializer.Initialize();

			app.UseMvc();

			var builder = new ConfigurationBuilder()
			   .SetBasePath(env.ContentRootPath)
			   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
			   .AddEnvironmentVariables();

			this._Configuration = builder.Build();
		}

		private void InitializeContainer(IServiceCollection services)
		{
			services.AddScoped<IDbContextFactory, DbContextFactory>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			List<IServiceConfiguration> services2Config = new List<IServiceConfiguration>();

			services2Config.Add(new AppService.ServiceConfiguration());
			services2Config.Add(new Domain.ServiceConfiguration());
			services2Config.Add(new Data.ServiceConfiguration());

			foreach (var service in services2Config)
			{
				service.InitializeContainer(services, _Configuration);
			}
		}
	}
}
