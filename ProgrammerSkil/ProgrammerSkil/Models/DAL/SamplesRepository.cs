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
    public class SamplesRepository : Base, ISamplesRepository
    {
        #region  Field
        private readonly DbSet<TBL_Samples> _Sampleses;
        private readonly DbSet<TBL_User> _Users;

        #endregion  Field

        #region constructor
        public SamplesRepository(ProgrammerSkilContext db)
        {
            _Sampleses = _db.TBL_Samples;
            _Users = _db.TBL_User;

        }
        #endregion constructor





        /// <summary>
        /// Get Samaples By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_Samples> GetById(int Id)
        {
            return await _Sampleses
               .AsNoTracking()
                .Where(c => c.Id == Id)
                 .Include(c => c.User)
                      .Include(c => c.Images)
                         .FirstOrDefaultAsync();
        }






        /// <summary>
        /// getAll
        /// </summary>
        /// <returns></returns>
        public async Task<List<TBL_Samples>> GetAll()
        {
            return await _Sampleses
                .AsNoTracking()
                    .Include(c => c.User)
                        .Include(c => c.Images)
                              .Include(c => c.Images)
                                    .ToListAsync();
        }





        /// <summary>
        ///    GetAllList
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<TBL_Samples>> GetAllForPagination(int Page, int PageSize)
        {
            return await _Sampleses
                        .AsNoTracking()
                            .OrderBy(c => c.Id)
                                .Skip((Page - 1) * PageSize)
                                     .Take(PageSize)
                                         .Include(c => c.User)
                                             .Include(c => c.Images).ToListAsync();
        }





        /// <summary>
        ///    get count of item in database
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _Sampleses.AsNoTracking().Count();
        }





        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Sample"></param>
        /// <returns></returns>
        public async Task<TBL_Samples> Create(TBL_Samples Sample)
        {
            if (Sample == null) return null;
            var newSample = _Sampleses.Add(Sample);
            var result = await SaveChangeAsync();
            if (!result)
                return null;
            return newSample;
        }





        /// <summary>
        ///Update
        /// </summary>
        /// <param name="Sample"></param>
        /// <returns></returns>
        public async Task<TBL_Samples> Update(TBL_Samples Sample)
        {
            var sampleFromDb = await _Sampleses.FindAsync(Sample.Id);
            if (sampleFromDb == null) return null;
            sampleFromDb.Id = Sample.Id;
            sampleFromDb.Link = Sample.Link;
            sampleFromDb.Name = Sample.Name;
            sampleFromDb.Description = Sample.Description;
            sampleFromDb.UserId = Sample.UserId;
            //sampleFromDb.Images = Sample.Images;

            try
            {
                var result = await SaveChangeAsync();
                return sampleFromDb;
            }
            catch
            {
                return null;
            }
        }





        public bool IsSampleExist(int Id)
        {
            return _Sampleses.AsNoTracking().Any(c => c.Id == Id);
        }





        /// <summary>
        ///Remove
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> Remove(int Id)
        {
            var sampleFromDb = await _Sampleses.FindAsync(Id);
            if (sampleFromDb == null)
                return false;
            _db.TBL_Images.Where(c => c.SamplesId == sampleFromDb.Id).Load();
            if (sampleFromDb == null)
                return false;
            _Sampleses.Remove(sampleFromDb);
            var result = await SaveChangeAsync();
            if (result)
            {
                return true;
            }
            else
                return false;
        }





        /// <summary>
        ///Remove
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_Samples> Remove2(int Id)
        {
            var sampleFromDb = await _Sampleses.FindAsync(Id);
            if (sampleFromDb == null)
                return null;
            _db.TBL_Images.Where(c => c.SamplesId == sampleFromDb.Id).Load();
            if (sampleFromDb == null)
                return null;
            _Sampleses.Remove(sampleFromDb);
            var result = await SaveChangeAsync();
            if (result)
            {
                return sampleFromDb;
            }
            else
                return null;
        }





    }
}