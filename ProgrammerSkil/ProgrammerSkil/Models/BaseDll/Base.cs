using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.BaseDll
{
    public class Base : IBase
    {

        #region  Field
        protected readonly ProgrammerSkilContext _db;
        #endregion  Field
        #region constructor
        public Base()
        {
            _db = new ProgrammerSkilContext();
        }
        #endregion constructor

        public bool saveChanges()
        {
            //try
            //{
                var result = _db.SaveChanges();
                if (result > 0)
                    return true;
                return false;
            //}
            //catch
            //{
            //    return false;
            //}
        }





        public async Task<bool> SaveChangeAsync()
        {
            //try
            //{
            var result = await _db.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
            //}
            //catch
            //{
            //    return false;
            //}
        }





        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                    //  context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }





    }
}