using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL
{
    public class Person
    {
		public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name(s)")]
        public string MiddleNames { get; set; }

        [Required(ErrorMessage = "Please enter your surname")]
        public string Surname { get; set; }

        [DisplayName("Current Status")]
        [Required(ErrorMessage = "Please enter your current status")]
        public string CurrentStatus { get; set; }

        [DisplayName("Show On Site")]
        public bool ShowOnSite { get; set; }

		public virtual List<PhoneNumber> PhoneNumbers { get; set; }
		public virtual List<Address> Addresses { get; set; }
		public virtual List<Email> Emails { get; set; }

		public static class CurrentStatuses
		{
			public static string Member { get { return "Member"; } }
			public static string ASFMember { get { return "ASF Member"; } }
			public static string FormerMember { get { return "Former Member"; } }
			public static string Other { get { return "Other"; } }
		}
	}
}
