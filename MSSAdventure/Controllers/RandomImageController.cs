using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MSSAdventure.Models;

namespace MSSAdventure.Controllers
{
    public class RandomImageController : Controller
    {
        //
        // GET: /RandomImage/

		public FileContentResult GetImage()
        {
			// Create a random code and store it in the Session object.
			Session["CaptchaImageText"] = GenerateRandomCode();
			// Create a CAPTCHA image using the text stored in the Session object.
			using (RandomImage ci = new RandomImage(Session["CaptchaImageText"].ToString(), 300, 75))
			{
				return File(ci.Image.ToByteArray(System.Drawing.Imaging.ImageFormat.Jpeg), "image/jpeg");
			}
        }

		private string GenerateRandomCode()
		{
			Random r = new Random();
			string s = "";
			for (int j = 0; j < 5; j++)
			{
				int i = r.Next(3);
				int ch;
				switch (i)
				{
					case 1:
						ch = r.Next(0, 9);
						s = s + ch.ToString();
						break;
					case 2:
						ch = r.Next(65, 90);
						s = s + Convert.ToChar(ch).ToString();
						break;
					case 3:
						ch = r.Next(97, 122);
						s = s + Convert.ToChar(ch).ToString();
						break;
					default:
						ch = r.Next(97, 122);
						s = s + Convert.ToChar(ch).ToString();
						break;
				}
				r.NextDouble();
				r.Next(100, 1999);
			}
			return s;
		}

    }
}
