
namespace MGA.Data.Context
{
	public class DbInitializer : IDbInitializer
	{
		private readonly MGAContext _context;

		public DbInitializer(
			MGAContext context
			)
		{
			_context = context;
		}

		public void Initialize()
		{
			if(_context.Database == null)
				_context.Database.EnsureCreated();
		}
	}
}
