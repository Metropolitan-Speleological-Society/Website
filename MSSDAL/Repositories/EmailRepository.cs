using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL.Repositories
{
	public interface IEmailRepository : IRepository<Email>
	{
	}

	public class EmailRepository : Repository<Email>, IEmailRepository
	{
		public EmailRepository(MSSContext context) : base(context) { }
	}
}
