using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL.Repositories
{
	public interface ITripRepository : IRepository<Trip>
	{
	}

	public class TripRepository : Repository<Trip>, ITripRepository
	{
		public TripRepository(MSSContext context) : base(context) { }
	}
}
