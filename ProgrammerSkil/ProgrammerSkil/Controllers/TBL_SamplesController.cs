using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using JwtAttribute;
using ProgrammerSkil.Models;
using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.ExtentionMethods;
using ProgrammerSkil.Models.ExtentionMethods.Attribute;
using ProgrammerSkil.Models.Utilities;
using ProgrammerSkil.Models.ViewModel.Sample;

namespace ProgrammerSkil.Controllers
{
    [RoutePrefix("api/sample")]
    public class TBL_SamplesController : ApiController
    {
        #region Field
        private readonly ISamplesManager _sampleManager;
        private readonly IImageManager _imageManager;
        #endregion Field 

        #region Constractor   
        public TBL_SamplesController(ISamplesManager sampleManager
            , IImageManager imageManager)
        {
            _sampleManager = sampleManager;
            _imageManager = imageManager;
        }
        #endregion Constractor





        // GET: api/sample/5
        [ResponseType(typeof(SampleViewModel))]
        [HttpGet]
        [AllowAnonymous]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> GetSample(int id)
        {
            var Sample = await _sampleManager.GetById(id);
            if (Sample == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "نمونه کار یافت نشد");
            return Request.CreateResponse(HttpStatusCode.OK, Sample);
        }





        // GET: api/skill/
        [ResponseType(typeof(SampleViewModel))]
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> GetAllSkills()
        {
            var Sample = await _sampleManager.GetAll();
            return Ok(Sample);
        }





        // GET: api/skill/
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        //[Route("{page:int}&&{pageSize:int}")]
        [Route("")]
        public async Task<IHttpActionResult> GetAllDamplesForPagibnation(int page, int pageSize)
        {
            var Samples = await _sampleManager.GetAllForPagination(page, pageSize);
            var DTO = new
            {
                Samples = Samples,
                Count = _sampleManager.Count()
            };
            return Ok(DTO);
        }





        [HttpPost]
        [JwtAuthentication2]
        [Authorize(Roles = Static.AdminRole)]
        [Route("")]
        public async Task<HttpResponseMessage> Create([FromBody] PostSampleViewModel SampleViewModel)
        {
            if (!ModelState.IsValid)
            {
                var Results = _sampleManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
                //return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            //var ValidateFil = _imageManager.ValidateFile(SampleViewModel);

            var Result = await _sampleManager.Create(SampleViewModel);
            if (Result == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "خطایی در ثبت نمونه کار رخ داده");

            /////   لیست از   فایل بگیر
            //if (Result == null)
            //{
            //    return BadRequest("مشکلی در ثبت نمونه کار رخ داده");
            //}
            //if (SampleViewModel.FIles.Count > 0)
            //    return
            return Request.CreateResponse(HttpStatusCode.BadRequest, Result);
        }





        [HttpPut]
        [JwtAuthentication2]
        [Authorize(Roles = Static.AdminRole)]
        [Route("{Id:int}")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Update(int Id, [FromBody] PostSampleViewModel PostSampleViewModel)
        {
            if (!ModelState.IsValid)
            {
                var Results = _sampleManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
                //return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            //var ValidateFil = _imageManager.ValidateFile(SampleViewModel);

            if (Id != PostSampleViewModel.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var IsExist = _sampleManager.IsSampleExist(PostSampleViewModel.Id);
            if (!IsExist)
                return Request.CreateResponse(HttpStatusCode.NotFound, "نمونه کار یافت نشد");

            var Sample = await _sampleManager.Update(PostSampleViewModel);
            if (Sample == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "مشکلی در ثبت تغییرات ");

            /////   لیست از   فایل بگیر
            //var ImageResult =   _imageManager.Update(Request.Files, Sample.Id);
            //if (SampleViewModel.FIles.Count > 0)
            //    return

            return Request.CreateResponse(HttpStatusCode.OK, Sample);
        }






        //[HttpDelete]
        //[BasicAuthentication(Role: "admin")]
        //[Route("{Id:int}")]
        //[ResponseType(typeof(string))]
        //public async Task<IHttpActionResult> Delete(int Id)
        //{
        //    var Result = await _sampleManager.Delete(Id);
        //    if (!Result)
        //    {
        //        return BadRequest("خطا در حذف نمونه کار ");
        //    }
        //    return Ok("حذف موفقیت آمیز ");
        //}





        [HttpDelete]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{Id:int}")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Delete(int Id)
        {
            var sample = await _sampleManager.GetById(Id);
            if (sample == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "نمونه کار یافت نشد");
            var Result = await _sampleManager.Delete2(Id, sample);
            if (Result == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "خطا در حذف نمونه کار");
            }
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }
        //"~/Upload/SampleImage/token.txt"
        //"~/Upload/SampleImage/LinkNet.txt"
        //"~/Upload/SampleImage/a.txt"


    }
}