using MGA.CrossCutting.Data;
using System;
using System.Collections.Generic;

namespace MGA.Domain.Interfaces.Services
{
    public interface IUserService : IDisposable
	{
		IList<User> Get();
		User Get(int id);
		User Post(User value);
		User Put(User value);
		void Delete(int id);


		User Login(string email, string password);
	}
}
