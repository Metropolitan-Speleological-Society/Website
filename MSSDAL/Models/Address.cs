using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL
{
	public class Address
	{
		public Guid Id { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string Suburb { get; set; }
		public string State { get; set; }
		public string Postcode { get; set; }
		public string Country { get; set; }
	}
}
