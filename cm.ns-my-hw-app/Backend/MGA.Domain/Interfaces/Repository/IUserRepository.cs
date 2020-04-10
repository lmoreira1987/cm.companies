

using MGA.CrossCutting.Data;

namespace MGA.Domain.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
		User Login(string email, string password);
    }
}
