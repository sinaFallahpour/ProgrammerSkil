using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using JwtAttribute;
using ProgrammerSkil.Models;
using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.ExtentionMethods;
using ProgrammerSkil.Models.ExtentionMethods.Attribute;
using ProgrammerSkil.Models.ViewModel.Skill;

namespace ProgrammerSkil.Controllers
{
    [RoutePrefix("api/skill")]
    public class TBL_SkillController : ApiController
    {
        #region Field
        private readonly ISkillManager _skillManager;
        #endregion Field 

        #region Constractor   
        public TBL_SkillController(ISkillManager skillManager)
        {
            _skillManager = skillManager;
        }
        #endregion Constractor


        //[BasicAuthentication]
        //[MyAuthorize(Roles = "admin")]
        //[Route("api/AllMaleEmployees")]
        //public HttpResponseMessage GetAllMaleEmployees()
        //{
        //    var dsds = GetCurerentUser.GetUserId();
        //    return Request.CreateResponse(HttpStatusCode.OK, "value");
        //}







        // GET: api/skill/5
        [ResponseType(typeof(SkillViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> GetSkill(int id)
        {
            ////get Current Claims Principal
            //var Idemtity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            ////get Current Claims Principal  value
            //var RoleClaimValue = Idemtity.Claims.Where(c => c.Type == "UserRole")
            //         .Select(c => c.Value).SingleOrDefault();
            //var UserNameClaimValue = Idemtity.Claims.Where(c => c.Type == "UserName")
            //        .Select(c => c.Value).SingleOrDefault();
            //var IdClaimValue = Idemtity.Claims.Where(c => c.Type == "_Id")
            //        .Select(c => c.Value).SingleOrDefault();



            var Skill = await _skillManager.GetById(id);
            if (Skill == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "مهارت یافت نشد");
            }
            return Request.CreateResponse(HttpStatusCode.OK, Skill);
            //return Ok(Skill);
        }





        // GET: api/skill/
        [ResponseType(typeof(SkillViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("")]
        public async Task<IHttpActionResult> GetAllSkills()
        {
            var Skill = await _skillManager.GetAll();
            var DTO = new
            {
                Skills = Skill,
                Count = _skillManager.Count()
            };
            return Ok(DTO);
        }







        // GET: api/skill/
        [ResponseType(typeof(SkillViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        //[Route("{page:int}&&{pageSize:int}")]
        [Route("")]
        public async Task<IHttpActionResult> GetAllSkillsForPagibnation(int page, int pageSize)
        {
            var Skill = await _skillManager.GetAllForPagination(page, pageSize);
            var DTO = new
            {
                Skills = Skill,
                Count = _skillManager.Count()
            };
            return Ok(DTO);
        }





        [HttpPost]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("")]
        public async Task<HttpResponseMessage> Create([FromBody] SkillViewModel SkillViewModel)
        {
            if (!ModelState.IsValid)
            {
                var Results = _skillManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
                //return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var Result = await _skillManager.Create(SkillViewModel);
            if (!Result)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "خطایی در ایجاد مهارت رخ داده");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ثبت موفقیت آمیز");
        }





        [HttpPut]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{Id:int}")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Update(int Id, [FromBody] SkillViewModel SkillViewModel)
        {
            if (!ModelState.IsValid)
            {
                var Results = _skillManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest,Results);
                //return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (Id != SkillViewModel.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var Result = await _skillManager.Update(SkillViewModel);
            if (!Result)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "خطایی در ثبت تغییرات");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ثیت موفقیت آمیز تغییرات");
        }





        [HttpDelete]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{Id:int}")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Delete(int Id)
        {
            var Skill = await _skillManager.GetById(Id);
            if (Skill == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, " مهارت یافت نشد");
            var Result = await _skillManager.Delete(Id);
            if (!Result)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "خطا در حذف ");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "حذف موفقیت آمیز ");
        }





    }
}