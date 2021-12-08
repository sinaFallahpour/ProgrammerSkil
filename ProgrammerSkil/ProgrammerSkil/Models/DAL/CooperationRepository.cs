using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL
{
    public class CooperationRepository : Base, ICooperationRepository
    {
        #region  Field
        private readonly DbSet<TBL_Cooperation> _Cooperations;
        #endregion  Field

        #region constructor
        public CooperationRepository()
        {
            _Cooperations = _db.TBL_Cooperation;
        }
        #endregion constructor





        /// <summary>
        /// Get Cooperation By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_Cooperation> GetById(int Id)
        {
            return await _Cooperations.Where(c => c.Id == Id)
                         .AsNoTracking() .Include(c => c.User).FirstOrDefaultAsync();
        }






        /// <summary>
        /// getAll
        /// </summary>
        /// <returns></returns>
        public async Task<List<TBL_Cooperation>> GetAll()
        {
            return await _Cooperations.AsNoTracking().Include(c => c.User).ToListAsync();
        }





        /// <summary>
        ///    GetAllList
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<TBL_Cooperation>> GetAllForPagination(int Page, int PageSize)
        {
            return await _Cooperations
               .AsNoTracking()
               .OrderBy(c => c.Id)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize).Include(c => c.User)
                .ToListAsync();
        }





        public bool IsExist(int Id)
        {
            return _Cooperations.AsNoTracking().Any(c => c.Id == Id);
        }




        /// <summary>
        /// getall item in database
        /// </summary>
        public int Count()
        {
            return _Cooperations.AsNoTracking().Count();
        }




        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Cooperation"></param>
        /// <returns></returns>
        public async Task<TBL_Cooperation> Create(TBL_Cooperation Cooperation)
        {
            if (Cooperation == null) return null;
            Cooperation.Status = CooperationStatus.NoChecked;
            var newCooperation = _Cooperations.Add(Cooperation);

            var result = await SaveChangeAsync();
            if (!result)
                return null;
            return newCooperation;
        }





        /// <summary>
        ///Update
        /// </summary>
        /// <param name="cooperation"></param>
        /// <returns></returns>
        public async Task<TBL_Cooperation> Update(TBL_Cooperation cooperation)
        {
            var cooperationFromDb = await _Cooperations.FindAsync(cooperation.Id);
            if (cooperationFromDb == null) return null;
            cooperationFromDb.Id = cooperation.Id;
            cooperationFromDb.Status = cooperation.Status;
            cooperationFromDb.User = cooperation.User;
            cooperationFromDb.UserId = cooperation.UserId;
            cooperationFromDb.Text = cooperation.Text;
            cooperationFromDb.ResumeLink = cooperation.ResumeLink;
            var result = await SaveChangeAsync();
            if (!result)
                return null;
            return cooperationFromDb;
        }


        public async Task<TBL_Cooperation> ChangeCooporationStatus(int Id, CooperationStatus Status)
        {
            var cooporationFromDb = await _Cooperations.FindAsync(Id);
            if (cooporationFromDb == null)
                return null;
            cooporationFromDb.Status = Status;
            try
            {
                await SaveChangeAsync();
                return cooporationFromDb;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        ///Remove
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
            public async Task<bool> Remove(int Id)
        {
            var cooperationFromDb = await _Cooperations.FindAsync(Id);
            if (cooperationFromDb == null)
                return false;
            _Cooperations.Remove(cooperationFromDb);
            var result = await SaveChangeAsync();
            if (!result)
                return false;
            return true;
        }





    }
}