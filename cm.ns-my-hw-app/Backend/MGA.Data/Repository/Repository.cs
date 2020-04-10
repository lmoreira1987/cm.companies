using MGA.CrossCutting.Data;
using MGA.Data.Context;
using MGA.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MGA.Data.Repository
{
	public abstract class Repository<T> : IRepository<T> where T : BaseEntity, new()
	{
		protected IDbContextFactory dbFactory;
		protected MGAContext Db;
		protected DbSet<T> DbSet;

		protected Repository(IDbContextFactory dbFactory)
		{
			Db = dbFactory.GetMainContext();
			DbSet = Db.Set<T>();
		}

		protected User GetUser(int userID)
		{
			var returnValue = Db.Users.FirstOrDefault(u => u.ID == userID);

			if (returnValue == null)
				throw new ArgumentOutOfRangeException(nameof(userID), "User ID");

			return returnValue;
		}

		public virtual T Post(T obj)
		{
			var objAdd = DbSet.Add(obj);
			SaveChanges();
			return objAdd.Entity;
		}

		public virtual T Put(T obj)
		{
			var updEntry = Db.Entry<T>(obj);
			updEntry.State = EntityState.Modified;

			SaveChanges();

			return updEntry.Entity;
		}

		public virtual IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
		{
			return DbSet.Where(predicate);
		}

		public virtual T Get(int id)
		{
			var returnValue = DbSet
				.FirstOrDefault(u => u.ID == id);

			return returnValue;
		}

		public virtual IList<T> Get()
		{
			return DbSet.ToList();
		}

		public virtual void Delete(int id)
		{
			var obj = Get(id);
			DbSet.Remove(obj);
			SaveChanges();
		}

		public int SaveChanges()
		{
			int returnValue = 0;

			try
			{
				returnValue = Db.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception("SaveChanges error: ", e);
			}

			return returnValue;
		}

		public void Dispose()
		{
			Db.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
