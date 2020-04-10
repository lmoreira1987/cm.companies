using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MGA.Data.Context
{
    public class DbContextFactory : IDbContextFactory
    {
        public DbContextFactory(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public IConfiguration Configuration { get { return _Configuration; } }
        protected IConfiguration _Configuration;

        public MGAContext GetMainContext()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<MGAContext>();
            options.UseSqlServer(connectionString); // , opt => opt.EnableRetryOnFailure()


            MGAContext dbContext = new MGAContext(options.Options);
            
            return dbContext;
        }
    }
}
