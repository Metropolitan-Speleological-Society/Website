using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL.Repositories
{
	public interface IMembershipRepository : IRepository<Membership>
	{
	}

	public class MembershipRepository : Repository<Membership>, IMembershipRepository
	{
		public MembershipRepository(MSSContext context) : base(context) { }
	}
}
