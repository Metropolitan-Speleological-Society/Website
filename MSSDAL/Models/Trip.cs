using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.DAL
{
	public class Trip
	{
		public Guid Id { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
		public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string Activity { get; set; }

        public string Organisation { get; set; }

        [DisplayName("Trip Leader")]
        public string TripLeader { get; set; }

        public string Notes { get; set; }

        [DisplayName("Location")]
        public string GenericLocation { get; set; }

        public string Grade { get; set; }

        public static class TripActivity
        {
            public static string Caving { get { return "Caving"; } }
            public static string Canyoning { get { return "Canyoning"; } }
            public static string Training { get { return "Training"; } }
            public static string Meeting { get { return "Meeting"; } }
            public static string Walking { get { return "Walking"; } }
            public static string Social { get { return "Social"; } }
            public static string Conference { get { return "Conference"; } }
            public static string Kayaking { get { return "Kayaking"; } }
            public static string Skiing { get { return "Skiing"; } }
            public static string Miscellaneous { get { return "Miscellaneous"; } }
        }

        public static class TripGrade
        {
            public static string Social { get { return "Social"; } }
            public static string Easy { get { return "Easy"; } }
            public static string EasyMedium { get { return "Easy - Medium"; } }
            public static string Medium { get { return "Medium"; } }
            public static string MediumHard { get { return "Medium - Hard"; } }
            public static string Hard { get { return "Hard"; } }
            public static string McGregor { get { return "McGregor"; } }
        }
	}
}
