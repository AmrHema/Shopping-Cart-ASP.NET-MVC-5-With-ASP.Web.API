using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chart_Leader.Repository.RepositoryS_
{
    public class GenericUnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly leader_Entities DBEntity;

        public GenericUnitOfWork()
        {
            DBEntity = new leader_Entities();
        }

        public DbContext Db
        {
            get { return DBEntity; }
        }


        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

    }
}