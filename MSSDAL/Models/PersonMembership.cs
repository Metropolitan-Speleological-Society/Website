using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL
{
	public class PersonMembership
	{
		public Guid Id { get; set; }
		public List<Person> People { get; set; }
		public List<Membership> Memberships { get; set; }
	}
}
