

using MGA.AppService.ViewModels;
using System;
using System.Collections.Generic;

namespace MGA.AppService.Interfaces
{
	public interface IUserAppService : IDisposable
	{
		IList<UserViewModel> Get();
		UserViewModel Get(int id);
		UserViewModel Post(UserViewModel value);
		UserViewModel Put(UserViewModel value);
		void Delete(int id);



		UserViewModel Login(string email, string password);
	}
}
