using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MSS.DAL
{
	public class Repository<TObject> : IRepository<TObject>
			where TObject : class
	{
		private bool shareContext = false;

		public Repository()
		{
			Context = new MSSContext();
		}

		public Repository(MSSContext context)
		{
			Context = context;
			shareContext = true;
		}

		protected MSSContext Context = null;

		protected DbSet<TObject> DbSet
		{
			get
			{
				return Context.Set<TObject>();
			}
		}

		public void Dispose()
		{
			if (shareContext && (Context != null))
				Context.Dispose();
		}

		public virtual IQueryable<TObject> All()
		{
			return DbSet.AsQueryable();
		}

		public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate)
		{
			return DbSet.Where(predicate).AsQueryable<TObject>();
		}

		public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> filter, out int total, int index = 0, int size = 50)
		{
			int skipCount = index * size;
			var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
			_resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
			total = _resetSet.Count();
			return _resetSet.AsQueryable();
		}

		public bool Contains(Expression<Func<TObject, bool>> predicate)
		{
			return DbSet.Count(predicate) > 0;
		}

		public virtual TObject Find(params object[] keys)
		{
			return DbSet.Find(keys);
		}

		public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
		{
			return DbSet.FirstOrDefault(predicate);
		}

		public virtual IQueryable<TObject> Include(Expression<Func<TObject, bool>> predicate)
		{
			return DbSet.Include(predicate);
		}

		public virtual IQueryable<TObject> Include(string path)
		{
			return DbSet.Include(path);
		}

		public virtual TObject Create(TObject TObject)
		{
			var newEntry = DbSet.Add(TObject);
			if (!shareContext)
				Context.SaveChanges();
			return newEntry;
		}

		public virtual TObject AddIfNotExists(TObject TObject, Expression<Func<TObject, bool>> predicate = null)
		{
			var exists = predicate != null ? DbSet.Any(predicate) : DbSet.Any();
			if (!exists)
			{
				return null;
			}

			var newEntry = DbSet.Add(TObject);
			if (!shareContext)
				Context.SaveChanges();
			return newEntry;
		}


		public virtual int Count
		{
			get
			{
				return DbSet.Count();
			}
		}

		public virtual int Delete(TObject TObject)
		{
			DbSet.Remove(TObject);
			if (!shareContext)
				return Context.SaveChanges();
			return 0;
		}

		public virtual int Update(TObject TObject)
		{
			var entry = Context.Entry(TObject);
			DbSet.Attach(TObject);
			entry.State = EntityState.Modified;
			if (!shareContext)
				return Context.SaveChanges();
			return 0;
		}

		public virtual int Delete(Expression<Func<TObject, bool>> predicate)
		{
			var objects = Filter(predicate);
			foreach (var obj in objects)
				DbSet.Remove(obj);
			if (!shareContext)
				return Context.SaveChanges();
			return 0;
		}

		public int Save()
		{
			try
			{
				return Context.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
				StringBuilder sb = new StringBuilder();

				foreach (var failure in ex.EntityValidationErrors)
				{
					sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
					foreach (var error in failure.ValidationErrors)
					{
						sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
						sb.AppendLine();
					}
				}

				throw new DbEntityValidationException(
					"Entity Validation Failed - errors follow:\n" +
					sb.ToString(), ex
				); // Add the original exception as the innerException
			}
		}

	}
}
