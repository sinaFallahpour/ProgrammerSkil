[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ProgrammerSkil.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ProgrammerSkil.App_Start.NinjectWebCommon), "Stop")]

namespace ProgrammerSkil.App_Start
{
    using System;
    using System.Web;
    using AutoMapper;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using ProgrammerSkil.Models.BLL;
    using ProgrammerSkil.Models.BLL.Interfaces;
    using ProgrammerSkil.Models.DLL;
    using ProgrammerSkil.Models.DLL.Interfaces;

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
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);

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
            //kernel.Bind<IDetail>().To<Detail>();
            //DAL
            kernel.Bind<ICooperationRepository>().To<CooperationRepository>();
            kernel.Bind<IImageaRepository>().To<ImageaRepository>();
            kernel.Bind<IFeedBackRepostory>().To<FeedBackRepostory>();
            kernel.Bind<ISamplesRepository>().To<SamplesRepository>();
            kernel.Bind<ISettingRepository>().To<SettingRepository>();
            kernel.Bind<ISkillRepository>().To<SkillRepository>();
            kernel.Bind<ISkillUserProfileRepository>().To<SkillUserProfileRepository>();
            kernel.Bind<IUserProfileRepository>().To<UserProfileRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IAuthRepository>().To<AuthRepository>();

            //BLL
            kernel.Bind<ICooperationManager>().To<CooperationManager>();
            kernel.Bind<IFeedBackManager>().To<FeedBackManager>();
            kernel.Bind<IImageManager>().To<ImageManager>();
            kernel.Bind<ISamplesManager>().To<SamplesManager>();
            kernel.Bind<ISettingManager>().To<SettingManager>();
            kernel.Bind<ISkillManager>().To<SkillManager>();
            kernel.Bind<ISkillUserProfileManager>().To<SkillUserProfileManager>();
            kernel.Bind<IUserManager>().To<UserManager>();
            kernel.Bind<IUserProfileManager>().To<UserProfileManager>();
            kernel.Bind<IAuthManager>().To<AuthManager>();

            kernel.Bind<IMapper>().To<Mapper>();

        }
    }
}
