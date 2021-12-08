using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL
{
    public class FeedBackRepostory : Base, IFeedBackRepostory
    {
        #region  Field
        private readonly DbSet<TBL_FeedBacks> _FeedBackses;
        #endregion  Field

        #region constructor
        public FeedBackRepostory(ProgrammerSkilContext db)
        {
            _FeedBackses = _db.TBL_FeedBacks;
        }
        #endregion constructor





        /// <summary>
        /// Get Skill By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_FeedBacks> GetById(int Id)
        {
            return await _FeedBackses.FindAsync(Id);
        }






        /// <summary>
        /// getAll
        /// </summary>
        /// <returns></returns>
        public async Task<List<TBL_FeedBacks>> GetAll()
        {
            return await _FeedBackses.AsNoTracking().ToListAsync();
        }





        /// <summary>
        ///    GetAllList
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<TBL_FeedBacks>> GetAllForPagination(int Page, int PageSize)
        {
            return await _FeedBackses
                      .AsNoTracking()
                         .OrderBy(c=>c.RegDate)
                              .Skip((Page - 1) * PageSize)
                                    .Take(PageSize).ToListAsync();
        }






        /// <summary>
        ///    get count of item in database
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _FeedBackses
                    .AsNoTracking()
                        .Count();
        }





        /// <summary>
        /// Create
        /// </summary>
        /// <param name="FeedBack"></param>
        /// <returns></returns>
        public async Task<bool> Create(TBL_FeedBacks FeedBack)
        {
            if (FeedBack == null) return false;
            _FeedBackses.Add(FeedBack);
            var result = await SaveChangeAsync();
            if (result)
            {
                return true;
            }
            else
                return false;
        }





        ///// <summary>
        /////Update
        ///// </summary>
        ///// <param name="User"></param>
        ///// <returns></returns>
        //public async Task<bool> Update(TBL_FeedBacks FeedBack)
        //{
        //    var skillFromDb = await _Fed.FindAsync(Skill.Id);
        //    if (skillFromDb == null) return false;
        //    skillFromDb.Id = Skill.Id;
        //    skillFromDb.Title = Skill.Title;
        //    var result = await SaveChangeAsync();
        //    if (result)
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}





        /// <summary>
        ///Remove
        /// </summary>
        /// <param name="FeedBack"></param>
        /// <returns></returns>
        public async Task<bool> Remove(int Id)
        {
            var feedBackFromDb = await _FeedBackses.FindAsync(Id);
            if (feedBackFromDb == null)
                return false;
            _FeedBackses.Remove(feedBackFromDb);
            var result = await SaveChangeAsync();
            if (result)
            {
                return true;
            }
            else
                return false;
        }





    }
}