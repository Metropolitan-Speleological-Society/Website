using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL.Repositories
{
	public interface IPersonRepository : IRepository<Person>
	{
	}

	public class PersonRepository : Repository<Person>, IPersonRepository
	{
		public PersonRepository(MSSContext context) : base(context) { }
	}
}
