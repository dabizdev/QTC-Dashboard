using System;
using Common.EF.Library;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

//using System.Data.Entity;

namespace Qtc.Dashboard.BusinessLayer.AppBaseClasses
{
    public class AppDataManager : EFDataManagerBase
    {
        public SqlEntities sqlEntity;
        public IDbContextTransaction trans;


        #region Transaction Methods
        public SqlEntities CreateTrans()
        {
            sqlEntity = new SqlEntities();
            trans = base.BeginTransaction(sqlEntity);
            return sqlEntity;
        }

        public void CommitTrans()
        {
            if (trans != null)
            {
                base.Commit(trans);
                trans.Dispose();
            }
            if (sqlEntity != null)
            {
                sqlEntity.Dispose();
            }
        }

        public void RollbackTrans()
        {
            if (trans != null)
            {
                base.Rollback(trans);
                trans.Dispose();
            }
            if (sqlEntity != null)
            {
                sqlEntity.Dispose();
            }
        }

        protected List<T> ExecuteSqlQueryInTrans<T>(string exceptionMsg = "") where T : class
        {
            List<T> ret = new List<T>();

            ret = base.ExecuteSqlQuery<T>(sqlEntity, exceptionMsg);

            return ret;
        }

        protected int ExecuteSqlCommandInTrans(string exceptionMsg, bool getIdentity = false, string identityParamName = "")
        {
            // Execute the action query
            RowsAffected = base.ExecuteSqlCommand(sqlEntity, exceptionMsg);
            return RowsAffected;
        }
        #endregion

        #region Commands with Db Transaction
        protected int ExecuteSqlCommandWithTransaction(SqlEntities db, string exceptionMsg, bool getIdentity = false, string identityParamName = "")
        {
            // Execute the action query
            RowsAffected = base.ExecuteSqlCommand(db, exceptionMsg);
            return RowsAffected;
        }

        protected int ExecuteSqlCommand(SqlEntities db, string exceptionMsg, bool getIdentity = false, string identityParamName = "")
        {
            // Execute the action query
            RowsAffected = base.ExecuteSqlCommand(db, exceptionMsg);

            return RowsAffected;
        }

        protected List<T> ExecuteSqlQuery<T>(SqlEntities db, string exceptionMsg = "") where T : class
        {
            List<T> ret = new List<T>();

            ret = base.ExecuteSqlQuery<T>(db, exceptionMsg);

            return ret;
        }

        protected T ExecuteScalar<T>(SqlEntities db, string exceptionMsg = "")
        {
            T ret = default(T);

            ret = base.ExecuteScalar<T>(db);

            return ret;
        }
        #endregion

        #region ExecuteSqlCommand Method
        protected int ExecuteSqlCommand(string exceptionMsg, bool getIdentity = false, string identityParamName = "")
        {

            // Create instance of DbContext
            using (SqlEntities db = new SqlEntities())
            {
                // Execute the action query
                RowsAffected = base.ExecuteSqlCommand(db, exceptionMsg);
            }

            return RowsAffected;
        }
        #endregion

        #region ExecuteSqlQuery Method
        protected List<T> SetSqlQuery<T>(string exceptionMsg = "") where T : class
        {
            List<T> ret = new List<T>();

            using (SqlEntities db = new SqlEntities())
            {
                ret = base.SetSqlQuery<T>(db, exceptionMsg);
            }

            return ret;
        }
        #endregion

        #region ExecuteSqlQuery Method
        protected List<T> ExecuteSqlQuery<T>(string exceptionMsg = "") where T : class
        {
            List<T> ret = new List<T>();

            using SqlEntities db = new SqlEntities();

            ret = base.ExecuteSqlQuery<T>(db, exceptionMsg);

            return ret;
        }
        #endregion

        #region ExecuteScalar Method
        protected T ExecuteScalar<T>(string exceptionMsg = "")
        {
            T ret = default(T);

            using (SqlEntities db = new SqlEntities())
            {
                ret = base.ExecuteScalar<T>(db);
            }

            return ret;
        }
        #endregion

        #region ExecuteReader Method
        protected DbDataReader ExecuteReader(DbContext db, DbCommand cmd, string exceptionMsg = "")
        {
            return base.ExecuteDbReader(db, cmd, exceptionMsg);
        }
        #endregion

        #region ExecuteNonQuery Method
        protected int ExecuteNonQuery<T>(DbCommand cmd, string exceptionMsg = "") where T : class
        {
            using (SqlEntities db = new SqlEntities())
            {
                return base.ExecuteNonQuery(db, cmd, exceptionMsg);
            }
        }
        #endregion
    }
}
