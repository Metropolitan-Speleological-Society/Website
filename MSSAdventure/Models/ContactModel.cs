using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSSAdventure.Models
{
	public class ContactModel
	{
		[Required(ErrorMessage = "Please enter your name")]
		public string Name { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Please enter your email address")]
		[DataType(DataType.EmailAddress)]
		[DisplayName("Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter your comments")]
		[DataType(DataType.MultilineText)]
		public string Thoughts { get; set; }

		[Required]
		public string ImageCode { get; set; }
	}
}