using ProgrammerSkil.Models.BLL;
using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.Enums;
using ProgrammerSkil.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TAD_Security;

namespace ProgrammerSkil.Models.ExtentionMethods.Attribute
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        //public string _role { get; set; }


        //public BasicAuthenticationAttribute()
        //{

        //}

        //public BasicAuthenticationAttribute(string Role) : this()
        //{
        //    _role = Role;
        //}

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                var request = actionContext.Request;
                var authorization = request.Headers.Authorization;

                if (authorization == null || authorization.Scheme != "Bearer")
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "not authorizesd");
                    if (actionContext.Response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Unauthorized"));
                    }
                    return;
                }

                if (string.IsNullOrEmpty(authorization.Parameter))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Missing Jwt Token");
                    if (actionContext.Response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Unauthorized"));
                    }
                    return;
                }

                //validate for get user  from oken
                if (actionContext.Request.Headers.Authorization != null)
                {
                    var token = authorization.Parameter;
                    var principal = AuthenticateJwtToken2(token, out AuthType AuthType);

                    if (principal == null && AuthType == AuthType.NotAuthenticated)
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Token");
                    else if (principal == null && AuthType == AuthType.NotAuthorized)
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "عدم سطح دسترسی  .شما به این بخش دسترسی ندارید");
                    else
                    {

                        //var identity = new GenericIdentity(username);
                        //identity.AddClaim(new Claim("Email", UserDetails.Email));
                        //identity.AddClaim(new Claim(ClaimTypes.Name, UserDetails.UserName));
                        //identity.AddClaim(new Claim("ID", Convert.ToString(UserDetails.ID)));
                        //IPrincipal Newprincipal = new GenericPrincipal(identity, UserDetails.Roles.Split(','));

                        Thread.CurrentPrincipal = principal;
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.User = principal;
                        }

                        // setting current principle  
                        //Thread.CurrentPrincipal = principal;
                        //new GenericPrincipal(new GenericIdentity(principal.Identity.Name), null);
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "not authorized");
                }
            }
            catch (Exception)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, "sorry there is a problem from server");
            }
        }


        protected IPrincipal AuthenticateJwtToken2(string token, out AuthType AuthType)
        {
            AuthType = AuthType.NotAuthenticated;
            var IsAuthenticatedUser = IsUserAuthenticated(token, out ClaimsTypesViewModel usersClaim, out AuthType AuthenticationType);
            if (IsAuthenticatedUser)
            {
                // based on username to get more information from database in order to build local identity
                var claims = new List<Claim>
                {
                    //new Claim(ClaimTypes.Name, usersClaim.UserName),
                    //new Claim(ClaimTypes.Role, usersClaim.Role),
                    //new Claim("role", usersClaim.Role),
                    //new Claim("userName", usersClaim.UserName),
                    //new Claim("_Id", usersClaim.Id),
                     new Claim(ClaimTypes.Name, usersClaim.UserName),
                    new Claim(ClaimTypes.Role, usersClaim.Role),
                    new Claim("UserRole", usersClaim.Role),
                    new Claim("UserName", usersClaim.UserName),
                    new Claim("_Id", usersClaim.Id),
                    // Add more claims if needed: Roles, ...
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return user;
            }
            if (AuthenticationType == AuthType.NotAuthenticated)
            {
                AuthType = AuthType.NotAuthenticated;
                return null;
            }
            AuthType = AuthType.NotAuthorized;
            return null;
        }


        private bool IsUserAuthenticated(string token, out ClaimsTypesViewModel ClaimsTypes, out AuthType AuthenticationType)
        {
            var db = new ProgrammerSkilContext();
            //username = null;
            ClaimsTypes = null;
            AuthenticationType = AuthType.NotAuthenticated;
            //var simplePrinciple = JwtManager.GetPrincipal(token);
            var simplePrinciple = JwtManager.GetPrincipalFromToken(token);

            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;


            if (!identity.IsAuthenticated)
                return false;


            var usernameClaim = identity.FindFirst("UserName");
            var roleClaim = identity.Claims.Where(c => c.Type == "UserRole")
                    .Select(c => c.Value).SingleOrDefault();
            var IdClaim = identity.Claims.Where(c => c.Type == "_Id")
                    .Select(c => c.Value).SingleOrDefault();
            var SerialNumber = identity.Claims.Where(c => c.Type == "SerialNumber")
                          .Select(c => c.Value).SingleOrDefault();
            ClaimsTypes = new ClaimsTypesViewModel();
            ClaimsTypes.UserName = usernameClaim?.Value;
            ClaimsTypes.Role = roleClaim ?? "";
            ClaimsTypes.Id = IdClaim ?? "";
            ClaimsTypes.SerialNumber = SerialNumber ?? "";

            var result = string.IsNullOrEmpty(ClaimsTypes.UserName) || string.IsNullOrEmpty(ClaimsTypes.Role)
                                        || string.IsNullOrEmpty(ClaimsTypes.Id)||string.IsNullOrEmpty(ClaimsTypes.SerialNumber);
            if (result)
                return false;

            int userId;
            if (!int.TryParse(ClaimsTypes.Id, out userId))
                return false;

            var user = db.TBL_User.FirstOrDefault(c => c.Id == userId);
            if (user == null)
                return false;
            
            if (user.SerialNumber != SerialNumber)
                return false;

            var HashToken = token.GetHash();
            var TokenFromDb = db.TBL_Token.First(c => c.AccessTokenHash == HashToken);
            if (TokenFromDb == null)
                return false;

            //if Authorize with Role
            //if (!string.IsNullOrEmpty(_role))
            //{
            //    if (ClaimsTypes.Role != _role && user.RoleType != ClaimsTypes.Role)
            //    {
            //        AuthenticationType = AuthType.NotAuthorized;
            //        return false;
            //    }
            //}

            AuthenticationType = AuthType.Authenticated;
            // More validate to check whether username exists in system
            return true;
        }
    }
}