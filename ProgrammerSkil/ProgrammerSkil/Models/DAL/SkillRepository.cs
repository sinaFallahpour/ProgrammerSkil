using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL
{
    public class SkillRepository : Base, ISkillRepository
    {
        #region  Field
        private readonly DbSet<TBL_Skill> _Skills;
        #endregion  Field

        #region constructor
        public SkillRepository(ProgrammerSkilContext db)
        {
            _Skills = _db.TBL_Skill;
        }
        #endregion constructor





        /// <summary>
        /// Get Skill By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_Skill> GetById(int Id)
        {
            return await _Skills.FindAsync(Id);
        }






        /// <summary>
        /// getAll
        /// </summary>
        /// <returns></returns>
        public async Task<List<TBL_Skill>> GetAll()
        {
            return await _Skills.AsNoTracking().ToListAsync();
        }





        /// <summary>
        ///    GetAllList
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<TBL_Skill>> GetAllForPagination(int Page, int PageSize)
        {
            return await _Skills
                .AsNoTracking()
                    .OrderBy(c => c.Id)
                        .Skip((Page - 1) * PageSize)
                            .Take(PageSize).ToListAsync();
        }





        /// <summary>
        ///    get count of item in database
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _Skills.AsNoTracking().Count();
        }





        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Skill"></param>
        /// <returns></returns>
        public async Task<bool> Create(TBL_Skill Skill)
        {
            if (Skill == null) return false;
            _Skills.Add(Skill);
            var result = await SaveChangeAsync();
            if (result)
            {
                return true;
            }
            else
                return false;
        }





        /// <summary>
        ///Update
        /// </summary>
        /// <param name="Skill"></param>
        /// <returns></returns>
        public async Task<bool> Update(TBL_Skill Skill)
        {
            var skillFromDb = await _Skills.FindAsync(Skill.Id);
            if (skillFromDb == null) return false;
            skillFromDb.Id = Skill.Id;
            skillFromDb.Title = Skill.Title;
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
        public async Task<bool> Remove(int Id)
        {
            var skillFromDb =await  _Skills
                    .Where(c => c.Id == Id)
                        .Include(c=>c.SkillUserProfile)
                            .FirstOrDefaultAsync();
            
            /*FindAsync(Id);*/
            if (skillFromDb == null)
                return false;
            _Skills.Remove(skillFromDb);
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