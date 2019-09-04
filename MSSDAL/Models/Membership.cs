using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL
{
	public class Membership
	{
		public Guid Id { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public string MembershipType { get; set; }

		public static class MembershipTypes
		{
			public static string FullSingle { get { return "Full Single"; } }
			public static string FullTwoParent { get { return "Full Two Parent Family"; } }
			public static string FullSingleParent { get { return "Full Single Parent Family"; } }
			public static string TrialSingle { get { return "Trial Single"; } }
			public static string TrialTwoParent { get { return "Trial Two Parent Family"; } }
			public static string TrialSingleParent { get { return "Trial Single Parent Family"; } }
		}
	}
}
