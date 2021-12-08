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
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using JwtAttribute;
using ProgrammerSkil.Models;
using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.ExtentionMethods;
using ProgrammerSkil.Models.ExtentionMethods.Attribute;
using ProgrammerSkil.Models.ViewModel.FeedBack;

namespace ProgrammerSkil.Controllers
{
    [RoutePrefix("api/FeedBack")]
    public class TBL_FeedBacksController : ApiController
    {
        #region Field
        private readonly IFeedBackManager _feedBackManager;
        #endregion Field 

        #region Constractor   
        public TBL_FeedBacksController(IFeedBackManager feedBackManager)
        {
            _feedBackManager = feedBackManager;
        }
        #endregion Constractor





        // GET: api/FeedBack/5
        [ResponseType(typeof(FeedBackViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> GetFeedBack(int id)
        {
            var FeedBack = await _feedBackManager.GetById(id);
            if (FeedBack == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "پیشنهاد یافت نشد");
            }
            return Request.CreateResponse(HttpStatusCode.OK, FeedBack);
        }





        // GET: api/TBL_User/5
        [ResponseType(typeof(FeedBackViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("")]
        public async Task<IHttpActionResult> GetAllFeedBacks()
        {
            var FeedBacks = await _feedBackManager.GetAll();
            return Ok(FeedBacks);
        }




        // GET: api/skill/
        //[ResponseType(typeof(SkillViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        //[Route("{page:int}&&{pageSize:int}")]
        [Route("")]
        public async Task<IHttpActionResult> GetAllFeddbacksForPagibnation(int page, int pageSize)
        {
            var Feedbacks = await _feedBackManager.GetAllForPagination(page, pageSize);
            var DTO = new
            {
                Feedbacks = Feedbacks,
                Count = _feedBackManager.Count()
            };
            return Ok(DTO);
        }





        [HttpPost]
        [Route("")]
        [JwtAuthentication2]
        [Authorize]
        public async Task<HttpResponseMessage> Create([FromBody] FeedBackViewModel FeedBackViewModel)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            if (!ModelState.IsValid)
            {
                var Results = _feedBackManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
                //return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var Result = await _feedBackManager.Create(FeedBackViewModel);
            if (!Result)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "خظایی در ثبت اطلاعات");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ثبت موفقیت آمیز");
        }





        [HttpDelete]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{Id:int}")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Delete(int Id)
        {
            var FeedBack = await _feedBackManager.GetById(Id);
            if (FeedBack == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, " یافت نشد");
            }

            var Result = await _feedBackManager.Delete(Id);
            if (!Result)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "خطا در حذف ");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "حذف موفقیت آمیز ");
        }





    }
}