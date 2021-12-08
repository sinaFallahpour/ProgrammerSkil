using ProgrammerSkil.Models;
using ProgrammerSkil.Models.BLL;
using ProgrammerSkil.Models.ExtentionMethods.Attribute;
using ProgrammerSkil.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using TAD_Security;

namespace JwtAttribute
{
    public class JwtAuthentication2 :Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
                return;

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
                return;
            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtToken(token);

            if (principal == null)
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);

            else
            {
                context.Principal = principal;
                //Thread.CurrentPrincipal = principal;
                //if (HttpContext.Current != null)
                //{
                //    HttpContext.Current.User = principal;
                //}
            }
        }





        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            if (ValidateToken(token, out ClaimsTypesViewModel usersClaim))
            {
                // based on username to get more information from database in order to build local identity
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, usersClaim.UserName),
                    new Claim(ClaimTypes.Role, usersClaim.Role),
                    new Claim("UserRole", usersClaim.Role),
                    new Claim("UserName", usersClaim.UserName),
                    new Claim("_Id", usersClaim.Id),
                    // Add more claims if needed: Roles, ...
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }





        private static bool ValidateToken(string token, out ClaimsTypesViewModel ClaimsTypes)
        {
            var db = new ProgrammerSkilContext();
            ClaimsTypes = null;

            var simplePrinciple = JwtManager.GetPrincipalFromToken(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst("UserName");
            var roleClaim = identity.Claims.Where(c => c.Type == "UserRole").Select(c => c.Value).SingleOrDefault();
            var IdClaim = identity.Claims.Where(c => c.Type == "_Id").Select(c => c.Value).SingleOrDefault();
            var SerialNumber = identity.Claims.Where(c => c.Type == "SerialNumber").Select(c => c.Value).SingleOrDefault();

            ClaimsTypes = new ClaimsTypesViewModel();
            ClaimsTypes.UserName = usernameClaim?.Value;
            ClaimsTypes.Role = roleClaim ?? "";
            ClaimsTypes.Id = IdClaim ?? "";
            ClaimsTypes.SerialNumber = SerialNumber ?? "";

            var result = string.IsNullOrEmpty(ClaimsTypes.UserName) || string.IsNullOrEmpty(ClaimsTypes.Role)
                                        || string.IsNullOrEmpty(ClaimsTypes.Id) || string.IsNullOrEmpty(ClaimsTypes.SerialNumber);
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

            // More validate to check whether username exists in system

            return true;
        }





        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }
    }
}