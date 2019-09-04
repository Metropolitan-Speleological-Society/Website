using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL.Repositories
{
	public interface IAddressRepository : IRepository<Address>
	{
	}

	public class AddressRepository : Repository<Address>, IAddressRepository
	{
		public AddressRepository(MSSContext context) : base(context) { }
	}
}
