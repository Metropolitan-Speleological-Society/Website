using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL.Repositories
{
	public interface IPhoneNumberRepository : IRepository<PhoneNumber>
	{
	}

	public class PhoneNumberRepository : Repository<PhoneNumber>, IPhoneNumberRepository
	{
		public PhoneNumberRepository(MSSContext context) : base(context) { }
	}
}
