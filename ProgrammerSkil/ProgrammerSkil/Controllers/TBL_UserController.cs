using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using JwtAttribute;
using ProgrammerSkil.Models;
using ProgrammerSkil.Models.BLL;
using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.ExtentionMethods;
using ProgrammerSkil.Models.ExtentionMethods.Attribute;
using ProgrammerSkil.Models.Utilities;
using ProgrammerSkil.Models.ViewModel.Auth;
using ProgrammerSkil.Models.ViewModel.User;

namespace ProgrammerSkil.Controllers
{
    [RoutePrefix("api/user")]
    public class TBL_UserController : ApiController
    {





        #region Field
        private readonly IAuthManager _authManager;
        private readonly IUserManager _userManager;
        #endregion Field 

        #region Constractor   
        public TBL_UserController(IAuthManager authManager, IUserManager userManager)
        {
            //_db = new ProgrammerSkilContext();
            _authManager = authManager;
            _userManager = userManager;
        }
        #endregion Constractor





        // GET: api/FeedBack/5
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("user/{id:int}")]
        public async Task<HttpResponseMessage> GetUserById(int id)
        {
            var User = await _userManager.GetUserById(id);
            if (User == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "کاربر یافت نشد");
            }
            return Request.CreateResponse(HttpStatusCode.OK, User);
        }





        // GET: api/user/getAllUsers
        [ResponseType(typeof(UserViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("allUsers")]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            var Users = await _userManager.GetAllUser();
            return Ok(Users);
        }





        // GET: api/skill/
        //[ResponseType(typeof(SkillViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        //[Route("{page:int}&&{pageSize:int}")]
        [Route("allUsersList")]
        public async Task<IHttpActionResult> GetAllUsersForPagibnation(int page, int pageSize)
        {
            var Users = await _userManager.GetAllUserForPagination(page, pageSize);
            var DTO = new
            {
                Users = Users,
                Count = _userManager.UserCount()
            };
            return Ok(DTO);
        }





        // GET: api/FeedBack/5
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize]
        [Route("Programmer/{id:int}")]
        public async Task<HttpResponseMessage> GetProgrammerById(int id)
        {
            var CurrentUserId = GetCurerentUser.GetUserId();
            var CurrentUserRole = GetCurerentUser.GetUserRole();

            if (CurrentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "عدم هویت");
            var Programmer = await _userManager.GetProgrammerById(id);
            if (Programmer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "برنامه نویس یافت نشد");
            }
            if (CurrentUserRole != "admin" && Programmer.Id != CurrentUserId)
                return Request.CreateResponse(HttpStatusCode.Forbidden, " شما به این بخش دسترسی ندارید. باحساب خود وارد شوید");

            return Request.CreateResponse(HttpStatusCode.OK, Programmer);
        }






        // GET: api/user/getAllProgrammers
        [ResponseType(typeof(UserViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("allProgrammers")]
        public async Task<IHttpActionResult> getAllProgrammers()
        {
            var Users = await _userManager.GetAllProgrammer();
            return Ok(Users);
        }





        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        //[Route("{page:int}&&{pageSize:int}")]
        [Route("allProgrammersList")]
        public async Task<IHttpActionResult> GetAllProgrammerForPagibnation(int page, int pageSize)
        {
            var Programmers = await _userManager.GetAllProgrammerForPagination(page, pageSize);
            var DTO = new
            {
                Programmers = Programmers,
                Count = _userManager.ProgrammerCount()
            };
            return Ok(DTO);
        }





        // GET: api/FeedBack/5
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("admin/{id:int}")]
        public async Task<HttpResponseMessage> GetAdminById(int id)
        {
            var Admin = await _userManager.GetAdminById(id);
            if (Admin == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "ادمین یافت نشد");
            }
            return Request.CreateResponse(HttpStatusCode.OK, Admin);
        }





        // GET: api/FeedBack/5
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("adminDetails/{id:int}")]
        public async Task<HttpResponseMessage> GetAdminWithJoinById(int id)
        {
            var Admin = await _userManager.GetAdminWithJoinById(id);
            if (Admin == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "ادمین یافت نشد");
            }
            return Request.CreateResponse(HttpStatusCode.OK, Admin);
        }





        // GET: api/user/getAllAdmins
        [ResponseType(typeof(UserViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("allAdmins")]
        public async Task<IHttpActionResult> GetAllAdmins()
        {
            var Admins = await _userManager.GetAllAdmin();
            return Ok(Admins);
        }




        // GET: api/skill/
        //[ResponseType(typeof(SkillViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        //[Route("{page:int}&&{pageSize:int}")]
        [Route("allAdminList")]
        public async Task<IHttpActionResult> GetAllAdminForPagibnation(int page, int pageSize)
        {
            var Admins = await _userManager.GetAllAdnminForPagination(page, pageSize);
            var DTO = new
            {
                Admins = Admins,
                Count = _userManager.AdminCount()
            };
            return Ok(DTO);
        }





        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<HttpResponseMessage> RegisterUser([FromBody] RegisterViewModel RegisterViewModel)
        {
            if (!ModelState.IsValid)
            {
                var Results = _userManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
                //return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var result = _authManager.IsUsernameExist(RegisterViewModel.UserName);
            if (result)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "نام کاربری موجود است");

            var User = await _authManager.RegisterUser(RegisterViewModel);
            if (User != null)
            {

                string SerialNumber = Guid.NewGuid().ToString();
                var username = User.UserName;
                var id = User.Id;
                var role = User.RoleType;
                var token = JwtManager.GenerateToken(id, role, username, SerialNumber, 10);
                //user.JwtToken = token;
                var UserViewModel = await _userManager.SaveTokenForRegister(User, token, SerialNumber);
                if (UserViewModel == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "خطایی رخ داده");
                UserViewModel.JwtToken = token;
                return Request.CreateResponse(HttpStatusCode.OK, UserViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "خظایی رخ داده");
        }





        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<HttpResponseMessage> Login(LoginViewModel LoginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var Results = _userManager.Validate(ModelState);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
                }
                var userName = LoginViewModel.UserName;
                var password = LoginViewModel.Password;
                var User = await _userManager.CredentialForUser(userName, password);

                if (User != null)
                {
                    //var user = await _userManager.GetUserById();
                    string SerialNumber = Guid.NewGuid().ToString();
                    var username = User.UserName;
                    var id = User.Id;
                    var role = User.RoleType;
                    var token = JwtManager.GenerateToken(id, role, username, SerialNumber, 10);
                    //User.JwtToken = token;

                    var userDetails = await _userManager.SaveToken(User, token, SerialNumber);
                    if (userDetails == null)
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "خطایی رخ داده");
                    userDetails.JwtToken = token;
                    return Request.CreateResponse(HttpStatusCode.OK, userDetails);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "کاربر یافت نشد");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;




            }
        }





        [HttpPost]
        [AllowAnonymous]
        [Route("AdminLogin")]
        public async Task<HttpResponseMessage> AdminLogin(LoginViewModel LoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                var Results = _userManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
            }

            var userName = LoginViewModel.UserName;
            var password = LoginViewModel.Password;
            var AdminDetails = await _userManager.CrentialForAdmin(userName, password);
            if (AdminDetails != null)
            {
                //var user = await _userManager.GetUserById();

                string SerialNumber = Guid.NewGuid().ToString();
                var username = AdminDetails.UserName;
                var id = AdminDetails.Id;
                var role = AdminDetails.RoleType;
                var token = JwtManager.GenerateToken(id, role, username, SerialNumber, 10);
                //AdminDetails.JwtToken = token;
                //return Ok(AdminDetails);

                var UserViewModel = await _userManager.SaveTokenForRegister(AdminDetails, token, SerialNumber);
                if (UserViewModel == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "خطایی رخ داده");
                UserViewModel.JwtToken = token;
                return Request.CreateResponse(HttpStatusCode.OK, UserViewModel);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "کاربر یافت نشد");
        }





        [HttpPut]
        [JwtAuthentication2]
        [MyAuthorize(Roles = Static.ProgrammerRole)]
        [Route("EditProgrammerProfile/{Id:int}")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> EditProgrammerProfile(int Id, [FromBody] ProgrammerUpdateProfileViewModel Profile)
        {
            if (!ModelState.IsValid)
            {
                var Results = _userManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
            }
            //check Kon file Ersai ra
            ///var  res=_userManage.checkFile
            ///if(res)

            if (Id != Profile.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var result = await _userManager.IsUserExistById(Profile.Id);
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "کاربر یافت نشد");
            var isUserNameExist = _authManager.IsUsernameExistForEdit(Profile.UserName, Profile.Id);
            if (isUserNameExist)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "نام کاربری موجود است");
            if (result.RoleType != Static.ProgrammerRole)
                return Request.CreateResponse(HttpStatusCode.Forbidden, "عدم دسترسی ");
            var IsSkillsExist = _userManager.IsSkillsExist(Profile.SkillsId);
            if (!IsSkillsExist)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "مهارت ها یافت نشد");

            //این خظ بعدا اینطوریبست
            //var result = _userManager.UpdateProgrammerProfile(Profile,File)
            var Result = await _userManager.UpdateProgrammerProfile(Profile);
            if (Result == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "خطایی در ثبت تغییرات");
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }





        /// <summary>
        /// تغیر نقش به  برنامه نویس
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPost]
        [JwtAuthentication2]
        [Route("LogOut")]
        public IHttpActionResult LogOut()
        {
            var token = Request.Headers.Authorization.Parameter;
            var result = _userManager.LogOut(token);
            if (result)
                return Ok("خروج موفقیت آمیز");
            return BadRequest("خظایی رخ داده");
        }





        /// <summary>
        /// تغیر نقش به  ادمین
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPut]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("ChangeRoleToAdmin/{UserId:int}")]
        public async Task<HttpResponseMessage> ChangeRoleToAdmin(int UserId)
        {
            if (!ModelState.IsValid)
            {
                var Results = _userManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
            }
            var UserFromDb = await _userManager.IsUserExistById(UserId);
            if (UserFromDb == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "کاربرب یافت نشد");
            }


            if (UserFromDb.RoleType == Static.AdminRole)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "شما در نقش  ادمین هستید");

            var user = await _userManager.ChangeRoleToAdmin(UserFromDb);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, " خظایی رخ داده است");
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }





        /// <summary>
        /// تغیر نقش به  برنامه نویس
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPut]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("ChangeRoleToProgrammer/{UserId:int}")]
        public async Task<HttpResponseMessage> ChangeRoleToProgrammer(int UserId)
        {
            if (!ModelState.IsValid)
            {
                var Results = _userManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
            }
            var UserFromDb = await _userManager.IsUserExistById(UserId);
            if (UserFromDb == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "کاربرب یافت نشد");

            if (UserFromDb.RoleType == Static.ProgrammerRole)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "شما در نقش برنامه نویس هستید");

            var user = await _userManager.ChangeRoleToProgrammer(UserFromDb);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, " خظایی رخ داده است");
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }





        /// <summary>
        /// تغییر نقش به  برنامه نویس
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPut]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("ChangeRoleToUser/{UserId:int}")]
        public async Task<HttpResponseMessage> ChangeRoleToUser(int UserId)
        {
            if (!ModelState.IsValid)
            {
                var Results = _userManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
            }
            var UserFromDb = await _userManager.IsUserExistById(UserId);
            if (UserFromDb == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "کاربرب یافت نشد");
            if (UserFromDb.RoleType == Static.UserRole)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "شما در نقش  کاربر عادی هستید");

            var user = await _userManager.ChangeRoleToUser(UserFromDb);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, " خظایی رخ داده است");
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }





        /// <summary>
        /// تغییر نقش به  برنامه نویس
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPut]
        [JwtAuthentication2]
        [MyAuthorize]
        [Route("ChangePassword")]
        public async Task<HttpResponseMessage> ChangePassword(ChangePasswordViewModel ChangePasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                var Results = _userManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
            }

            var CurrentUserId = GetCurerentUser.GetUserId();
            var Result = _userManager.IsUsertExistForChangePassword(CurrentUserId, ChangePasswordViewModel.OldPasswrod);
            if (Result == false)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "پسورد درست را وارد کنید");

            var ChangeResult = await _userManager.ChangePasswrod((int)CurrentUserId, ChangePasswordViewModel.NewPassword);
            if (!ChangeResult)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "خطایی در تغییر پسورد رخ اده");

            var response = new
            {
                newPassword = ChangePasswordViewModel.NewPassword
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }






        [HttpDelete]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{Id:int}")]
        //[ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Delete(int Id)
        {
            var curentUserId = GetCurerentUser.GetUserId();

            var user = await _userManager.GetUserById(Id);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "کاربر یافت نشد");
            }

            //var curentUserId = GetCurerentUser.GetUserId();
            if (user.RoleType == Static.AdminRole && user.Id != curentUserId)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "اجازه حذف ادمین دیگر را ندارید");

            var DeletedUser = await _userManager.Delete(user.Id);
            if (DeletedUser == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "خطا در خذف کاربر");
            return Request.CreateResponse(HttpStatusCode.OK, DeletedUser);
        }














        /// <summary>
        /// اینکد ارور برای ولیدشن رر دیتا بیس روبهت نشونمیده
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        //[HttpDelete]
        //[BasicAuthentication(Role: "admin")]
        //[Route("{Id:int}")]
        ////[ResponseType(typeof(string))]
        //public async Task<HttpResponseMessage> Delete(int Id)
        //{

        //    try
        //    {
        //        var user = await _userManager.GetUserById(Id);
        //        if (user == null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound, "کاربر یافت نشد");
        //        }
        //        var curentUserId = GetCurerentUser.GetUserId();
        //        if (user.RoleType == Static.AdminRole && user.Id != curentUserId)
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "اجازه حذف ادمین دیگر را ندارید");

        //        var DeletedUser = await _userManager.Delete(user.Id);
        //        if (DeletedUser == null)
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "کاربر یافت نشد");
        //        return Request.CreateResponse(HttpStatusCode.OK, DeletedUser);
        //    }
        //    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
        //    {
        //        Exception raise = dbEx;
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                string message = string.Format("{0}:{1}",
        //                    validationErrors.Entry.Entity.ToString(),
        //                    validationError.ErrorMessage);
        //                // raise a new exception nesting  
        //                // the current instance as InnerException  
        //                raise = new InvalidOperationException(message, raise);
        //            }
        //        }
        //        throw raise;
        //    }

        //}





    }
}