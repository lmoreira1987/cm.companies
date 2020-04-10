using AutoMapper;
using MGA.AppService.ViewModels;
using MGA.CrossCutting.Data;

namespace MGA.AppService.AutoMapper
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserViewModel>().ReverseMap();
		}
	}
}
