using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL
{
    public class SettingRepository : Base, ISettingRepository
    {
        #region  Field
        private readonly DbSet<TBL_Setting> _Settings;
        private readonly DbSet<TBL_SocialMedia> _SocialMedia;

        #endregion  Field

        #region constructor
        public SettingRepository(ProgrammerSkilContext db)
        {
            _Settings = _db.TBL_Setting;
            _SocialMedia = _db.TBL_SocialMedia;
        }
        #endregion constructor





        /// <summary>
        /// Get Setting By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_Setting> GetById(int Id)
        {
            return await _Settings.Where(c => c.Id == Id)
                .Include(c => c.SocialMedias).FirstOrDefaultAsync();
        }





        /// <summary>
        /// getAll
        /// </summary>
        /// <returns></returns>
        public async Task<List<TBL_Setting>> GetAll()
        {

            return await _Settings.Include(c => c.SocialMedias).ToListAsync();
        }





        /// <summary>
        ///    GetAllList
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<TBL_Setting>> GetAllForPagination(int Page, int PageSize)
        {
            return await _Settings
                .Skip((Page - 1) * PageSize)
                .Take(PageSize).Include(c => c.SocialMedias).ToListAsync();
        }





        ///// <summary>
        ///// Create
        ///// </summary>
        ///// <param name="Skill"></param>
        ///// <returns></returns>
        //public async Task<bool> Create(TBL_Skill Skill)
        //{
        //    if (Skill == null) return false;
        //    _Skills.Add(Skill);
        //    var result = await SaveChangeAsync();
        //    if (result)
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}





        /// <summary>
        ///update
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task<TBL_Setting> Update(TBL_Setting setting)
        {
            var settingfromdb = await  _Settings.Where(c=>c.Id==setting.Id).Include(c=>c.SocialMedias).FirstOrDefaultAsync();
            if (settingfromdb == null) return null;
            settingfromdb.Id = setting.Id;
            settingfromdb.SocialMedias = setting.SocialMedias;
            settingfromdb.Phone = setting.Phone;
            settingfromdb.UserId = setting.UserId;
            settingfromdb.User = setting.User;
            settingfromdb.AboutUs = setting.AboutUs;

            //var SocialFromDb = _SocialMedia.Where(c => c.SettingId == settingfromdb.Id);
            //foreach (var item in SocialFromDb)
            //{
            //    item.Setting = null;
               
            //}
            //settingfromdb.SocialMedias = setting.SocialMedias;

            var result = await SaveChangeAsync();
            if (!result)
                return null;

            //settingfromdb.SocialMedias = setting.SocialMedias;
            return settingfromdb;
        }



        ///// <summary>
        /////Remove
        ///// </summary>
        ///// <param name="User"></param>
        ///// <returns></returns>
        //public async Task<bool> Remove(int Id)
        //{
        //    var skillFromDb = await _Skills.FindAsync(Id);
        //    if (skillFromDb == null)
        //        return false;
        //    _Skills.Remove(skillFromDb);
        //    var result = await SaveChangeAsync();
        //    if (result)
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}





        public bool IsExists(int id)
        {
            return _Settings.Count(e => e.Id == id) > 0;
        }






        public ICollection<TBL_SocialMedia> IsSoCialMediasMatched(List<int> socialMedias)
        {
            var AllSocialMedia = new List<TBL_SocialMedia>();
            foreach (var item in socialMedias)
            {
                var Social = _SocialMedia.Where(c => c.Id == item).FirstOrDefault();
                if (Social == null)
                    return null;
                AllSocialMedia.Add(Social);
            }
            return AllSocialMedia;
            //var SoCialIdFromDb = _SocialMedia.Select(c => c.Id).ToList();
            //if (socialMedias == SoCialIdFromDb)
            //    return SoCialIdFromDb;
            //return null;
        }





    }
}