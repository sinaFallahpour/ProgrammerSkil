using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using ProgrammerSkil.Models.BLL;
using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL;
using ProgrammerSkil.Models.DLL.Interfaces;

namespace Ninject_MVC.Controllers
{
    public class NinjectController:DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectController()
        {
            ninjectKernel=new StandardKernel();
            AddBinding();
        }

        void AddBinding()
        {
            //DAL
            ninjectKernel.Bind<ICooperationRepository>().To<CooperationRepository>();
            ninjectKernel.Bind<IFeedBackRepostory>().To<FeedBackRepostory>();
            ninjectKernel.Bind<IImageaRepository>().To<ImageaRepository>();
            ninjectKernel.Bind<ISamplesRepository>().To<SamplesRepository>();
            ninjectKernel.Bind<ISettingRepository>().To<SettingRepository>();
            ninjectKernel.Bind<ISkillRepository>().To<SkillRepository>();
            ninjectKernel.Bind<ISkillUserProfileRepository>().To<SkillUserProfileRepository>();
            ninjectKernel.Bind<IUserProfileRepository>().To<UserProfileRepository>();
            ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
            ninjectKernel.Bind<IAuthRepository>().To<AuthRepository>();

            //BLL
            ninjectKernel.Bind<ICooperationManager>().To<CooperationManager>();
            ninjectKernel.Bind<IFeedBackManager>().To<FeedBackManager>();
            ninjectKernel.Bind<IImageManager>().To<ImageManager>();
            ninjectKernel.Bind<ISamplesManager>().To<SamplesManager>();
            ninjectKernel.Bind<ISettingManager>().To<SettingManager>();
            ninjectKernel.Bind<ISkillManager>().To<SkillManager>();
            ninjectKernel.Bind<ISkillUserProfileManager>().To<SkillUserProfileManager>();
            ninjectKernel.Bind<IUserManager>().To<UserManager>();
            ninjectKernel.Bind<IUserProfileManager>().To<UserProfileManager>();
            ninjectKernel.Bind<IAuthManager>().To<AuthManager>();

        }


        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController) ninjectKernel.Get(controllerType);
        }

    }
}