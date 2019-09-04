using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MSSAdventure.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MSS.DAL;
using MSS.DAL.Models;
using MSS.DAL.Repositories;
using MSSAdventure.MSSEmail;

namespace MSSAdventure.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IPersonRepository _personRepository;
        private ITripRepository _tripRepository;
        private IMSSEmail _email;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IPersonRepository personRepository, ITripRepository tripRepository, IMSSEmail email)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _personRepository = personRepository;
            _tripRepository = tripRepository;
            _email = email;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Adventures()
        {
            return View();
        }

        public ActionResult Caving()
        {
            return View();
        }

        public ActionResult Canyoning()
        {
            return View();
        }

        public ActionResult MountainBiking()
        {
            return View();
        }

        public ActionResult Bushwalking()
        {
            return View();
        }

        public ActionResult KiteFlying()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            CalendarModel cModel = new CalendarModel
            {
                Trips = _tripRepository.Filter(t => t.Organisation == "MSS" && t.StartDate > DateTime.Now).OrderBy(t => t.StartDate).ToList()
            };
            return View(cModel);
        }

        public ActionResult NullarborPhotos()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult HowToJoin()
        {
            return View();
        }

        public ActionResult Abseiling()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Index", "Members");
                    case SignInStatus.LockedOut:
                        ModelState.AddModelError("", "Apparently you've been locked out.");
                        break;
                    case SignInStatus.RequiresVerification:
                        ModelState.AddModelError("", "Apparently you require further verification.");
                        break;
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                        return View(model);
                }
            }
            return View(model);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            try
            {
                if (contact.ImageCode == Session["CaptchaImageText"].ToString())
                {
                    if (!ModelState.IsValid)
                    {
                        ViewBag.TheMessage = "There is a problem with the data you've entered. Please correct the problem and try again.";
                        return View(contact);
                    }
                    else
                    {
                        _email.SendEmail("webmaster@mssadventure.org.au", "membership@mssadventure.org.au", "MSS Website Visitor", "Name: " + contact.Name + "\rEmail: " + contact.Email + "\rPhone: " + contact.Phone + "\rComments: " + contact.Thoughts);
                        ViewBag.TheMessage = "Thank you for contacting MSS. You will receive a reply shortly.";
                        return View("ContactConfirmation");
                    }
                }
                else
                {
                    ViewBag.TheMessage = contact.ImageCode.Length > 0 ? "The text you have entered did not match the text in the image. Please try again." : "";
                    return View(contact);
                }
            }
            catch (Exception ex)
            {
                ViewBag.TheMessage = ex.Message;
                return View(contact);
            }
        }
    }
}