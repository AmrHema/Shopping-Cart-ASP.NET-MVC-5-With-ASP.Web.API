using API_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace API_Project.Repository
{
    public class GenericRepository<Tbl_Entity> : IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        private leaderEntities_API db = new leaderEntities_API();
        private DbSet<Tbl_Entity> table;

        public GenericRepository()
        {
            this.table = db.Set<Tbl_Entity>();
        }
        public IEnumerable<Tbl_Entity> GetAll()
        {
            return table.ToList();
        }
        public void Add(Tbl_Entity entity)
        {
            table.Add(entity);
            db.SaveChanges();
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
                return db.Database.SqlQuery<Tbl_Entity>(query, parameters).ToList();
            }
            else
                return db.Database.SqlQuery<Tbl_Entity>(query).ToList();
        }

        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            table.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public virtual void Delete(object id)
        {
            Tbl_Entity entity = table.Find(id);
            Delete(entity);
            db.SaveChanges();
        }

        public virtual void Delete(Tbl_Entity entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                table.Attach(entity);
            }
            table.Remove(entity);
            db.SaveChanges();
        }
        public void Remove(Tbl_Entity entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
                table.Attach(entity);
            table.Remove(entity);
            db.SaveChanges();
        }

        public void RemovebyWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            Tbl_Entity entity = table.Where(wherePredict).FirstOrDefault();
            Remove(entity);
            db.SaveChanges();
        }

        public void RemoveRangeBywhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            List<Tbl_Entity> entity = table.Where(wherePredict).ToList();
            foreach (var ent in entity)
            {
                Remove(ent);
            }
            db.SaveChanges();
        }
        public virtual void Update(object id)
        {
            Tbl_Entity entity = table.Find(id);
            Update(entity);
        }
        public void Update(Tbl_Entity entity)
        {
            table.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            table.Where(wherePredict).ToList().ForEach(ForEachPredict);
            db.SaveChanges();
        }

        public IEnumerable<Tbl_Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<Tbl_Entity, bool>> wherePredict, Expression<Func<Tbl_Entity, int>> orderByPredict)
        {
            if (wherePredict != null)
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
            return db.Database.SqlQuery<Tbl_Entity>(query, parameters);
        }

        public IEnumerable<Tbl_Entity> StoreProcedure_SelectALL(string query)
        {
            return db.Database.SqlQuery<Tbl_Entity>(query);
        }

        public void Excute_Store_CUD(string query, params object[] parameters)
        {
            db.Database.ExecuteSqlCommand(query, parameters);
        }
    }
}