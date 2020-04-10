using System.Collections.Generic;
using MGA.AppService.Interfaces;
using MGA.AppService.ViewModels;
using MGA.Domain.Interfaces.Services;
using AutoMapper;
using MGA.CrossCutting.Data;

namespace MGA.AppService.Services
{
	public class UserAppService : IUserAppService
	{
		private readonly IUserService _userService;

		public UserAppService(IUserService userService)
		{
			_userService = userService;
		}

		public void Dispose()
		{
			_userService.Dispose();
		}

		public IList<UserViewModel> Get()
		{
			return Mapper.Map<IList<UserViewModel>>(_userService.Get());
		}

		public UserViewModel Get(int id)
		{
			return Mapper.Map<UserViewModel>(_userService.Get(id));
		}

		public UserViewModel Post(UserViewModel value)
		{
			var parameter = Mapper.Map<User>(value);
			return Mapper.Map<UserViewModel>(_userService.Post(parameter));
		}

		public UserViewModel Put(UserViewModel value)
		{
			var parameter = Mapper.Map<User>(value);
			return Mapper.Map<UserViewModel>(_userService.Put(parameter));
		}

		public void Delete(int id)
		{
			_userService.Delete(id);
		}

		public UserViewModel Login(string email, string password)
		{
			return Mapper.Map<UserViewModel>(_userService.Login(email, password));
		}
	}
}
