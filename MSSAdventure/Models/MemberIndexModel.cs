using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSSAdventure.Models
{
	public class MemberIndexModel
	{
		public MemberListModel memberList { get; set; }

        public bool CanCreateMember { get; set; }

        public bool CanCreateTrip { get; set; }
    }
}