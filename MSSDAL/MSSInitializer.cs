using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL
{
	public class MSSInitializer : System.Data.Entity.CreateDatabaseIfNotExists<MSSContext>
	{
		protected override void Seed(MSSContext context)
		{
		}
	}
}
