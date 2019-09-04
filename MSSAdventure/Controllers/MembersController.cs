using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

namespace MSSAdventure.Controllers
{
	[Authorize]
    public class MembersController : Controller
    {
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IPersonRepository _personRepository;
        private ITripRepository _tripRepository;
        private string[] _roles = { "SuperUser", "MemberAdmin", "TripAdmin" };

        #region Constructors
        public MembersController()
		{
		}

		public MembersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager, IPersonRepository personRepository, ITripRepository tripRepository)
		{
			UserManager = userManager;
			SignInManager = signInManager;
            RoleManager = roleManager;
			_personRepository = personRepository;
            _tripRepository = tripRepository;
		}
        #endregion

        #region Public Properties
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

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}
        #endregion

        #region Views
        // GET: Members
        public ActionResult Index()
        {
			return View();
		}

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Publications()
		{
			return View();
		}

		public ActionResult Newsletters()
		{
			return View();
		}

		public ActionResult LeaderChecklist()
		{
			return View();
		}

		public ActionResult FiftiethPublication()
		{
			return View();
		}

		public ActionResult ThirtiethPublication()
		{
			return View();
		}

		public ActionResult Constitution()
		{
			return View();
		}

		public ActionResult Library()
		{
			return View();
		}

		public ActionResult Membership()
		{
            MemberIndexModel model = new MemberIndexModel
            {
                memberList = new MemberListModel
                {
                    People = _personRepository.All().Where(p => p.ShowOnSite && p.CurrentStatus == Person.CurrentStatuses.Member).OrderBy(p => p.Surname).ToList()
                },
                CanCreateMember = UserCan(SecurityOption.CanCreateUser),
                CanCreateTrip = UserCan(SecurityOption.CanCreateTrip)
            };
			return View(model);
		}

		public ActionResult Merchandise()
		{
			return View();
		}

		public ActionResult Directions()
		{
			return View();
		}

		public ActionResult Links()
		{
			return View();
		}

        public ActionResult ReciprocalArrangements()
        {
            return View();
        }

        public ActionResult NewMember()
        {
            PersonModel personModel = new PersonModel();
            ViewBag.Title = "Add Person";
            personModel.person = new Person
            {
                Password = "forester"
            };
            return View("MemberDetails", personModel);
        }

        public ActionResult NewLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> NewLogin(string userName)
        {
            try
            {
                await RegisterUser(userName);
                ViewBag.Message = userName + " successfully created";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View("NewLoginResult");
        }

        public async Task<ActionResult> CreateUserRolesPage()
        {
            try
            {
                await CreateRoles();
                await CreateUserRoles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("Index");
        }

        public ActionResult MemberDetails ()
        {
            PersonModel personModel = new PersonModel();
            ViewBag.Title = "Add Person";
            personModel.person = new Person();
            ClaimsPrincipal claimsPrincipal = AuthenticationManager.User;
            string name = claimsPrincipal.Identity.Name;
            personModel.person = _personRepository.Find(p => p.Login == name);
            return View(personModel);
        }

        [HttpPost]
        public async Task<ActionResult> MemberDetails (Person person)
        {
            try
            {
                await RegisterUser(person.Login, person.Password);
                person.Id = Guid.NewGuid();
                _personRepository.Create(person);
                _personRepository.Save();
                ViewBag.Message = person.Login + " successfully created";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View("NewLoginResult");
        }

        public ActionResult UpdateTripDetails()
        {
            TripModel tripModel = new TripModel();
            ViewBag.Title = "Add Trip";
            return View(tripModel);
        }

        [HttpPost]
        public ActionResult UpdateTripDetails(Trip trip)
        {
            trip.Id = Guid.NewGuid();
            _tripRepository.Create(trip);
            _tripRepository.Save();
            return View("Index");
        }
        #endregion

        #region Supporting Functions
        private async Task RegisterUser(string uname)
        {
            await RegisterUser(uname, "forester");
        }

        private async Task RegisterUser(string uname, string pword)
        {
            var user = new ApplicationUser { UserName = uname };
            var existinguser = await UserManager.FindByNameAsync(uname);
            if (existinguser == null)
            {
                var result = await UserManager.CreateAsync(user, pword);
                if (!result.Succeeded)
                {
                    throw new Exception("Unable to create user " + uname + " because of " + result.Errors.ToString());
                }
            }
            else
            {
                throw new Exception("Unable to create user " + uname + " because they already exist in the system");
            }
        }

        private async Task CreateRoles ()
        {
            foreach (string role in _roles)
            {
                var theRole = await RoleManager.FindByNameAsync(role);
                if (theRole == null)
                    await RoleManager.CreateAsync(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(role));
            }
        }

        private async Task CreateUserRoles()
        {
            foreach (string role in _roles)
            {
                var theRole = await RoleManager.FindByNameAsync(role);
                if (theRole == null)
                    await RoleManager.CreateAsync(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(role));
                switch (role)
                {
                    case "SuperUser":
                        var user = await UserManager.FindByNameAsync("Rod.Smith");
                        await AssignRolesToUser(user.Id, new string[] { role });
                        break;
                    case "MemberAdmin":
                        var user2 = await UserManager.FindByNameAsync("Beth.Little");
                        var user3 = await UserManager.FindByNameAsync("Marilyn.Scott");
                        var user4 = await UserManager.FindByNameAsync("Cathi.Humphrey-Hood");
                        await AssignRolesToUser(user2.Id, new string[] { role });
                        await AssignRolesToUser(user3.Id, new string[] { role });
                        await AssignRolesToUser(user4.Id, new string[] { role });
                        break;
                    case "TripAdmin":
                        var user5 = await UserManager.FindByNameAsync("Chris.Johnstone");
                        await AssignRolesToUser(user5.Id, new string[] { role });
                        break;
                }
            }
            var user6 = await UserManager.FindByNameAsync("Marilyn.Scott");
            await AssignRolesToUser(user6.Id, new string[] { "MemberAdmin", "TripAdmin" });
        }

        //[ChildActionOnly]
        //public PartialViewResult _MemberList()
        //{
        //	AllMembers am = new AllMembers();
        //	return PartialView(am.GetMembers());
        //}

        private bool UserCan(SecurityOption role)
        {
            ClaimsPrincipal claimsPrincipal = AuthenticationManager.User;
            string id = claimsPrincipal.Identity.GetUserId();
            if (UserManager.IsInRole(id, "SuperUser"))
                return true;
            switch (role)
            {
                case SecurityOption.CanCreateUser:
                    if (UserManager.IsInRole(id, "MemberAdmin"))
                        return true;
                    break;
                case SecurityOption.CanCreateTrip:
                    if (UserManager.IsInRole(id, "TripAdmin"))
                        return true;
                    break;
                default:
                    return false;
            }
            return false;
        }

        private enum SecurityOption
        {
            CanCreateUser,
            CanCreateTrip
        }

        private async Task AssignRolesToUser(string id, string[] rolesToAssign)
        {
            if (rolesToAssign == null)
            {
                throw new Exception ("No roles specified");
            }

            ///find the user we want to assign roles to
            var appUser = await UserManager.FindByIdAsync(id);
            if (appUser == null)
            {
                throw new Exception ("User not found");
            }

            var rolesNotExist = rolesToAssign.Except(RoleManager.Roles.Select(x => x.Name)).ToArray();
            if (rolesNotExist.Count() > 0)
            {
                throw new Exception (string.Format("Roles '{0}' does not exist in the system", string.Join(",", rolesNotExist)));
            }

            ///remove user from current roles, if any
            var currentRoles = await UserManager.GetRolesAsync(appUser.Id);
            IdentityResult removeResult = await UserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());
            if (!removeResult.Succeeded)
            {
                throw new Exception ("Failed to remove user roles");
            }

            ///assign user to the new roles
            IdentityResult addResult = await UserManager.AddToRolesAsync(appUser.Id, rolesToAssign);
            if (!addResult.Succeeded)
            {
                throw new Exception ("Failed to add user roles");
            }
        }
        #endregion
    }
}