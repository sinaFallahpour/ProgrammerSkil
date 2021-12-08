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
using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.ExtentionMethods.Attribute;
using ProgrammerSkil.Models.ViewModel.Setting;

namespace ProgrammerSkil.Controllers
{
    [RoutePrefix("api/setting")]
    public class TBL_SettingController : ApiController
    {
        #region Field
        private readonly ISettingManager _settingManager;
        #endregion Field 

        #region Constractor   
        public TBL_SettingController(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }
        #endregion Constractor





        // GET: api/setting/5
        [ResponseType(typeof(SettingViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> GetSetting(int id)
        {
            var Setting = await _settingManager.GetById(id);
            if (Setting == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "تنظیمات یافت نشد");
            }
            return Request.CreateResponse(HttpStatusCode.OK, Setting);
        }




        // GET: api/setting/
        [ResponseType(typeof(SettingViewModel))]
        [HttpGet]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("")]
        public async Task<IHttpActionResult> GetAllSettings()
        {
            var Setting = await _settingManager.GetAll();
            return Ok(Setting);
        }





        [HttpPut]
        [JwtAuthentication2]
        [MyAuthorize(Roles = "admin")]
        [Route("{Id:int}")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Update(int Id, [FromBody] SettingForUpdateViewModel SettingViewModel)
        {
            if (!ModelState.IsValid)
            {
                var Results = _settingManager.Validate(ModelState);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Results);
                //return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (Id != SettingViewModel.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"تنظیمات یافت نشد");
            }
            var  IsExist= _settingManager.IsExist(SettingViewModel);
            if(!IsExist)
                return Request.CreateResponse(HttpStatusCode.NotFound, "تنظیمات یافت نشد");

            var IsSocialIdsExisst = _settingManager.IsSoCialMediasMatched(SettingViewModel.SocialMedias.ToList());
            if (IsSocialIdsExisst == null) {
               return  Request.CreateResponse(HttpStatusCode.BadRequest, "اطلاعات مربوط به لینک شبکه های جازی یافت نشد");
            }

            var Result = await _settingManager.Update(SettingViewModel , IsSocialIdsExisst);
            if (Result == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "مشکلی در ثبت تغییرات ");
            }
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }





    }
}