using Chart_Leader.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Chart_Leader.Repository.RepositoryS_
{
    public class GenericRepository<Tbl_Entity> : IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        internal DbSet<Tbl_Entity> table;
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            this.table = _unitOfWork.Db.Set<Tbl_Entity>();
        }
        public IEnumerable<Tbl_Entity> GetAll()
        {
            return table.ToList();
        }
        public void Add(Tbl_Entity entity)
        {
            table.Add(entity);
            _unitOfWork.Db.SaveChanges();
        }

        public virtual Tbl_Entity GetByID(object id)
        {
            return table.Find(id);
        }
        public int GetAllrecordCount()
        {
            return table.Count();
        }

        public IEnumerable<Tbl_Entity> GetAllRecords()
        {
            return table.ToList();
        }

        public IQueryable<Tbl_Entity> GetAllRecordsIQueryable()
        {
            return table;
        }

        public Tbl_Entity GetFirstorDefault(int recordId)
        {
            return table.Find(recordId);
        }

        public Tbl_Entity GetFirstorDefaultByParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return table.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<Tbl_Entity> GetListParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return table.Where(wherePredict).ToList();
        }

        public IEnumerable<Tbl_Entity> GetResultBySqlprocedure(string query, params object[] parameters)
        {
            if (parameters != null)
            {
                return _unitOfWork.Db.Database.SqlQuery<Tbl_Entity>(query, parameters).ToList();
            }
            else
                return _unitOfWork.Db.Database.SqlQuery<Tbl_Entity>(query).ToList();
        }

        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            table.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public virtual void Delete(object id)
        {
            Tbl_Entity entity = table.Find(id);
            Delete(entity);
            _unitOfWork.Db.SaveChanges();
        }

        public virtual void Delete(Tbl_Entity entity)
        {
            if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
            {
                table.Attach(entity);
            }
            table.Remove(entity);
            _unitOfWork.Db.SaveChanges();
        }
        public void Remove(Tbl_Entity entity)
        {
            if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
                table.Attach(entity);
            table.Remove(entity);
            _unitOfWork.Db.SaveChanges();
        }

        public void RemovebyWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            Tbl_Entity entity = table.Where(wherePredict).FirstOrDefault();
            Remove(entity);
            _unitOfWork.Db.SaveChanges();
        }

        public void RemoveRangeBywhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            List<Tbl_Entity> entity = table.Where(wherePredict).ToList();
            foreach(var ent in entity)
            {
                Remove(ent);
            }
            _unitOfWork.Db.SaveChanges();
        }
        public virtual void Update(object id)
        {
            Tbl_Entity entity = table.Find(id);
            Update(entity);
        }
        public void Update(Tbl_Entity entity)
        {
            table.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            _unitOfWork.Db.SaveChanges();
        }

        public void UpdateByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            table.Where(wherePredict).ToList().ForEach(ForEachPredict);
            _unitOfWork.Db.SaveChanges();
        }

        public IEnumerable<Tbl_Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<Tbl_Entity, bool>> wherePredict, Expression<Func<Tbl_Entity, int>> orderByPredict)
        {
          if(wherePredict!=null)
            {
                return table.OrderBy(orderByPredict).Where(wherePredict).ToList();

            }

            else
            {
                return table.OrderBy(orderByPredict).ToList();
            }
        }

        //Stored Procedure
       
        public IEnumerable<Tbl_Entity> StoreProcedure_SelectByParameters(string query, params object[] parameters)
        {
            return _unitOfWork.Db.Database.SqlQuery<Tbl_Entity>(query, parameters);
        }

        public IEnumerable<Tbl_Entity> StoreProcedure_SelectALL(string query)
        {
            return _unitOfWork.Db.Database.SqlQuery<Tbl_Entity>(query);
        }

        public void Excute_Store_CUD(string query, params object[] parameters)
        {
            _unitOfWork.Db.Database.ExecuteSqlCommand(query, parameters);
        }
    }
}