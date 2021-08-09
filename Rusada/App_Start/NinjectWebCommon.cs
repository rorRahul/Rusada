[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Rusada.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Rusada.App_Start.NinjectWebCommon), "Stop")]

namespace Rusada.App_Start
{
    using System;
    using System.Configuration;
    using System.Web;
    using Hangfire;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Rusada.DataLayer;
    using Rusada.ViewModelLayer.Repositories;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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
                string IQConnection = ConfigurationManager.ConnectionStrings["IRusadaConnection"].ConnectionString;
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
            string IQConnection = ConfigurationManager.ConnectionStrings["IRusadaConnection"].ConnectionString;

            kernel.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();//InRequestScope();
            kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();// InRequestScope();

            //DI Injection Through Constructor
            kernel.Bind<IAircraftRepository>().To<AircraftRepository>().InBackgroundJobScope().WithConstructorArgument("connectionString", IQConnection);


            Hangfire.GlobalConfiguration.Configuration.UseNinjectActivator(kernel);
        }
    }
}