using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGA.AppService.AutoMapper
{
	/// <summary>
	/// Version used 4.2.1
	/// Installed with Package Manager Console with the following instruction
	/// Install-Package AutoMapper -Version 4.2.1
	/// </summary>
	public class AutoMapperConfig
	{
		public static void RegisterMappings()
		{
            Mapper.Initialize(m =>
            {
                m.AddProfile<MappingProfile>();
            });
        }
	}
}
