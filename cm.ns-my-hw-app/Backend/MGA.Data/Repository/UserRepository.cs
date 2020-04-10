using MGA.CrossCutting.Data;
using MGA.Data.Context;
using MGA.Domain.Interfaces.Repository;
using System.Linq;

namespace MGA.Data.Repository
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(IDbContextFactory dbFactory) : base(dbFactory)
		{

		}

		public User Login(string email, string password)
		{
			return Db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
		}
	}
}
