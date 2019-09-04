using System.Web.Http;
using MSS.DAL;
using MSS.DAL.Repositories;
using MSSAdventure.MSSEmail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MSSAdventure.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MSSAdventure.NinjectWebCommon), "Stop")]

namespace MSSAdventure
{
	using System;
	using System.Web;

	using Microsoft.Web.Infrastructure.DynamicModuleHelper;

	using Ninject;
	using Ninject.Web.Common;
	using Ninject.Web.Common.WebHost;

	public static class NinjectWebCommon
	{
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			try
			{
				GlobalConfiguration.Configuration.DependencyResolver = kernel.Get<System.Web.Http.Dependencies.IDependencyResolver>();

				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

				RegisterServices(kernel);
				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}

		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
		{
			kernel.Bind<MSSContext>().ToSelf().InRequestScope();
			kernel.Bind<IPersonRepository>().To<PersonRepository>();
			kernel.Bind<ITripRepository>().To<TripRepository>();
            kernel.Bind<IMSSEmail>().To<MSS_Email>();
            kernel.Bind<ApplicationUserManager>().ToMethod(ctx => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
			kernel.Bind<ApplicationSignInManager>().ToMethod(ctx => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>());
            kernel.Bind<ApplicationRoleManager>().ToMethod(ctx => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>());
        }
    }
}
