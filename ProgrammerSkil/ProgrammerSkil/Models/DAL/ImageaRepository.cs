using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.DLL
{
    public class ImageaRepository: Base, IImageaRepository
    {
        #region  Field
        private readonly DbSet<TBL_Images> _Images;
        #endregion  Field

        #region constructor
        public ImageaRepository(ProgrammerSkilContext db)
        {
            _Images = _db.TBL_Images;
        }
        #endregion constructor
    }
}