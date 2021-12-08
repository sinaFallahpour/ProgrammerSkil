using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace ProgrammerSkil.Models.ExtentionMethods
{
    public static class GetCurerentUser
    {





        public static IEnumerable<Claim> GetUserClaims()
        {

            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            //Getting the ID value
            return identity.Claims;
        }






        /// <summary>
        /// گرفتن آیدی کاربر فعلی
        /// </summary>
        /// <returns></returns>
        public static int? GetUserId()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var Id = identity.Claims.Where(c => c.Type == "_Id")
                .Select(c => c.Value).SingleOrDefault();
            int userId;
            if (int.TryParse(Id, out userId))
                return userId;
            return null;
        }




        /// <summary>
        /// گرفتن نقش کاربر فعلی
        /// </summary>
        /// <returns></returns>
        public static string GetUserRole()
        {
            //get Current Claims Principal
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var Role = identity.Claims.Where(c => c.Type == "UserRole")
                     .Select(c => c.Value).SingleOrDefault();
            return Role;
        }





        /// <summary>
        /// گرفتن نام کاربری کاربر فعلی
        /// </summary>
        /// <returns></returns>
        public static string GetUserUserName()
        {
            //get Current Claims Principal
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var UserName = identity.Claims.Where(c => c.Type == "UserName")
                     .Select(c => c.Value).SingleOrDefault();
            return UserName;
        }





        //####################   
        //az inja be payein ba thread ast



        /// <summary>
        /// گرفتن  تمامی کلیم های کاربر فعلی
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Claim> GetUserClasimsWithThread()
        {
            //get Current Claims Principal
            var Idemtity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            return Idemtity.Claims;
        }



        /// <summary>
        /// گرفتن آیدی کاربر فعلی
        /// </summary>
        /// <returns></returns>
        public static int? GetUserIdWithThread()
        {
            //get Current Claims Principal
            var Idemtity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var Id = Idemtity.Claims.Where(c => c.Type == "_Id")
                 .Select(c => c.Value).SingleOrDefault();
            int userId;
            if (int.TryParse(Id, out userId))
                return userId;
            return null;
        }




        /// <summary>
        /// گرفتن نقش کاربر فعلی
        /// </summary>
        /// <returns></returns>
        public static string GetUserRoleWithThread()
        {
            //get Current Claims Principal
            var Idemtity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var Role = Idemtity.Claims.Where(c => c.Type == "UserRole")
                     .Select(c => c.Value).SingleOrDefault();
            return Role;
        }





        /// <summary>
        /// گرفتن نام کاربری کاربر فعلی
        /// </summary>
        /// <returns></returns>
        public static string GetUserUserNameWithThread()
        {
            //get Current Claims Principal
            var Idemtity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var UserName = Idemtity.Claims.Where(c => c.Type == "UserName")
                     .Select(c => c.Value).SingleOrDefault();
            return UserName;
        }





    }
}