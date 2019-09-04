using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL
{
	public class MSSContext : DbContext
	{
		public MSSContext() : base("MSSContext")
		{
		}

		public DbSet<Person> People { get; set; }
		public DbSet<PhoneNumber> PhoneNumbers { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Email> Emails { get; set; }
		public DbSet<Membership> Memberships { get; set; }
		public DbSet<PersonMembership> PersonMemberships { get; set; }
		public DbSet<Trip> Trips { get; set; }
	}
}
