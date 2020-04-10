
using System.Collections.Generic;
using MGA.CrossCutting.Data;
using MGA.Domain.Interfaces.Repository;
using MGA.Domain.Interfaces.Services;

namespace MGA.Domain.Services
{
    public class UserService : IUserService
    {
		protected readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
        {
			_userRepository = userRepository;
		}

		public void Dispose()
		{
			_userRepository.Dispose();
		}

		public IList<User> Get()
		{
			return _userRepository.Get();
		}

		public User Get(int id)
		{
			return _userRepository.Get(id);
		}

		public User Post(User value)
		{
			return _userRepository.Post(value);
		}

		public User Put(User value)
		{
			return _userRepository.Put(value);
		}

		public void Delete(int id)
		{
			_userRepository.Delete(id);
		}

		public User Login(string email, string password)
		{
			return _userRepository.Login(email, password);
		}
	}
}
