using MGA.CrossCutting.Data;
using MGA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MGA.Domain.Interfaces.Repository
{
	public interface IRepository<T> : IDisposable where T : BaseEntity
	{
        T Post(T obj);
		IEnumerable<T> Search(Expression<Func<T, bool>> predicate);
		T Get(int id);
		IList<T> Get();
        T Put(T obj);
		void Delete(int id);		
		int SaveChanges();
	}
}
