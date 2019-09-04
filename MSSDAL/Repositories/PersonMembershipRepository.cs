using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL.Repositories
{
	public interface IPersonMembershipRepository : IRepository<PersonMembership>
	{
	}

	public class PersonMembershipRepository : Repository<PersonMembership>, IPersonMembershipRepository
	{
		public PersonMembershipRepository(MSSContext context) : base(context) { }
	}
}
