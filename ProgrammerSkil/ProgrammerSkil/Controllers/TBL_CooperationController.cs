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
using AutoMapper;
using JwtAttribute;
using ProgrammerSkil.Models;
using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.Enums;
using ProgrammerSkil.Models.ExtentionMethods.Attribute;
using ProgrammerSkil.Models.Utilities;
using ProgrammerSkil.Models.ViewModel.Cooperation;
using TAD_ExtentionMethods;

namespace ProgrammerSkil.Controllers
{
    [RoutePrefix("api/cooperation")]
    public class TBL_CooperationController : ApiController
    {
        #region Field
        private readonly ICooperationManager _cooperationManager;
        #endregion Field 

        #region Constractor   
        public TBL_CooperationController(ICooperationManager cooperationManager)
        {
            _cooperationManager = cooperationManager;
        }
        #endregion Constractor





        // GET: api/setting/5
        [ResponseType(typeof(CooperationViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> GetCoopertion(int id)
        {
            var Cooperation = await _cooperationManager.GetById(id);
            if (Cooperation == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, " یافت نشد");
            }
            return Request.CreateResponse(HttpStatusCode.OK, Cooperation);
        }





        // GET: api/setting/
        [ResponseType(typeof(List<CooperationViewModel>))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("")]
        public async Task<IHttpActionResult> GetAllCooperation()
        {
            var Coopertaion = await _cooperationManager.GetAll();
            return Ok(Coopertaion);
        }





        // GET: api/skill/
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        //[Route("{page:int}&&{pageSize:int}")]
        [Route("")]
        public async Task<IHttpActionResult> GetAllCoopreationserationsForPagibnation(int page, int pageSize)
        {
            var Cooperations = await _cooperationManager.GetAllForPagination(page, pageSize);
            var DTO = new
            {
                Cooperations = Cooperations,
                Count = _cooperationManager.Count()
            };
            return Ok(DTO);
        }






        // GET: api/skill/
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = Static.AdminRole)]
        [Route("changeStatus/{Id:int}/{Status}")]
        public async Task<HttpResponseMessage> ChangeCooporationStatus(int Id, CooperationStatus Status)
        {
            var StatusDescription = ExtentionMethods.GetEnumDescription(Status);
            if(StatusDescription == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "وضعیت رزومه قابل قبول نیست");
            var IsExist = _cooperationManager.IsExist(Id);
            if (!IsExist)
                return Request.CreateResponse(HttpStatusCode.NotFound, "نمونه کار یافت نشد");
            var Cooporation = await _cooperationManager.ChangeCooporationStatus(Id, Status);
            if (Cooporation == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "مشکلی در ثبت تغییرات ");

            return Request.CreateResponse(HttpStatusCode.OK, Cooporation);
        }






        [HttpPost]
        [JwtAuthentication2]
        [Authorize]
        [Route("")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Create([FromBody] CooperationViewModel CooperationViewModel)
        {
            if (!ModelState.IsValid)
            {
                var Results = _cooperationManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
            }

            CooperationViewModel.Status = _cooperationManager.ToEnumValue(CooperationStatus.NoChecked);
            //get Current Claims Principal
            var Idemtity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            var UserId = Idemtity.Claims.Where(c => c.Type == "_Id")
                    .Select(c => c.Value).SingleOrDefault();
            int userId;

            if (!int.TryParse(UserId, out userId))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "کاربر یافت نشد");

            CooperationViewModel.UserId = userId;

            var Result = await _cooperationManager.Create(CooperationViewModel);

            if (Result == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, " خطادر دریافت رزومه");
            var RedultDTO = new
            {
                Id = Result.Id,
                ResumeLink = Result.ResumeLink,
                Status = Result.Status,
                Text = Result.Text,
                UserId = Result.UserId
            };
            return Request.CreateResponse(HttpStatusCode.OK, RedultDTO);
        }





        [HttpDelete]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{Id:int}")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Delete(int Id)
        {
            var Cooperation = await _cooperationManager.GetById(Id);
            if (Cooperation == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, " یافت نشد");
            }
            var Result = await _cooperationManager.Delete(Id);
            if (!Result)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "خطا در حذف ");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "حذف موفقیت آمیز ");
        }





    }
}