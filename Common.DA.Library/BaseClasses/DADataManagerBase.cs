using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.SqlClient;
using Common.DA.Library.Exceptions;

namespace Common.DA.Library.BaseClasses
{
    public class DADataManagerBase : DACommonBase
    {
        #region Properties
        public string SQL { get; set; }
        public object IdentityGenerated { get; set; }
        public List<SqlParameter> Parameters { get; set; }
        public List<DAValidationMessage> ValidationMessages { get; set; }
        #endregion

        #region Init Method
        public override void Init()
        {
            base.Init();

            SQL = string.Empty;
            IdentityGenerated = null;
            Parameters = new List<SqlParameter>();
            ValidationMessages = new List<DAValidationMessage>();
        }
        #endregion

        #region AddParameter Methods
        public virtual void AddParameter(SqlParameter sqlParam)
        {
            Parameters.Add(sqlParam);
        }
        public virtual void AddParameter(string name, object value, bool isNullable)
        {
            if (!name.Contains("@"))
            {
                name = "@" + name;
            }
            Parameters.Add(new SqlParameter { ParameterName = name, Value = value, IsNullable = isNullable });
        }

        public virtual void AddParameter(string name, object value, bool isNullable, DbType type, ParameterDirection direction = ParameterDirection.Input)
        {
            if (!name.Contains("@"))
            {
                name = "@" + name;
            }
            Parameters.Add(new SqlParameter { ParameterName = name, Value = value, IsNullable = isNullable, DbType = type, Direction = direction });
        }
        #endregion

        #region GetParameter Method
        public SqlParameter GetParameter(string name)
        {
            if (!name.Contains("@"))
            {
                name = "@" + name;
            }

            name = name.ToLower();
            return Parameters.Find(p => p.ParameterName.ToLower() == name);
        }
        #endregion

        #region Transaction 
        public virtual IDbContextTransaction BeginTransaction(DbContext db)
        {
            return db.Database.BeginTransaction();
        }

        public void Commit(IDbContextTransaction trans)
        {
            trans.Commit();
        }

        public void Rollback(IDbContextTransaction trans)
        {
            trans.Rollback();
        }
        #endregion

        #region ExecuteSqlCommand Method
        public virtual int ExecuteSqlCommand(DbContext db, string exceptionMsg = "")
        {
            try
            {
                // Execute the dynamic SQL
                RowsAffected = db.Database.ExecuteSqlRaw(SQL, Parameters.ToArray<object>());
                //RowsAffected = db.Set().FromSqlRaw(SQL, Parameters.ToArray<object>()).ToList();
            }
            catch (Exception ex)
            {
                ThrowDbException(ex, db, exceptionMsg);
            }

            return RowsAffected;
        }
        #endregion

        #region SetSqlQuery Method
        public virtual List<T> SetSqlQuery<T>(DbContext db, string exceptionMsg = "") where T : class
        {
            List<T> ret = new List<T>();

            try
            {
                ret = db.Set<T>().FromSqlRaw(SQL, Parameters.ToArray<object>()).ToList<T>();
            }
            catch (Exception ex)
            {
                ThrowDbException(ex, db, exceptionMsg);
            }

            return ret;
        }
        #endregion

        #region ExecuteSqlQuery Method
        public virtual List<T> ExecuteSqlQuery<T>(DbContext db, string exceptionMsg = "") where T : class
        {
            List<T> ret = new List<T>();

            try
            {
                //ret = db.Database.SqlQuery<T>(SQL, Parameters.ToArray<object>()).ToList<T>();
                ret = db.Set<T>().FromSqlRaw(SQL, Parameters.ToArray<object>()).ToList();
            }
            catch (Exception ex)
            {
                ThrowDbException(ex, db, exceptionMsg);
            }

            return ret;
        }
        #endregion

        #region ExecuteScalar Method
        public virtual T ExecuteScalar<T>(DbContext db, string exceptionMsg = "")
        {
            T ret = default(T);

            try
            {
                //ret = db.Database.SqlQuery<T>(SQL, Parameters.ToArray<object>()).SingleOrDefault<T>();
            }
            catch (Exception ex)
            {
                ThrowDbException(ex, db, exceptionMsg);
            }

            return ret;
        }
        #endregion

        #region ExecuteSqlQuery Method
        public virtual DbDataReader ExecuteDbReader(DbContext db, DbCommand cmd, string exceptionMsg = "")
        {
            DbDataReader reader = null;
            try
            {
                cmd.CommandText = SQL;
                db.Database.OpenConnection();

                cmd.Parameters.AddRange(Parameters.ToArray<object>());
                // Run the sproc
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                ThrowDbException(ex, db, exceptionMsg);
            }

            return reader;
        }
        #endregion

        #region ExecuteNonQuery Method
        public virtual int ExecuteNonQuery(DbContext db, DbCommand cmd, string exceptionMsg = "")
        {
            int ret = 0;

            try
            {
                cmd.CommandText = SQL;
                db.Database.OpenConnection();
                cmd.Parameters.AddRange(Parameters.ToArray<object>());

                // Run the sproc
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ThrowDbException(ex, db, exceptionMsg);
            }

            return ret;
        }
        #endregion

        #region AddValidationMessage Method
        public DAValidationMessage AddValidationMessage(string propertyName, string message)
        {
            DAValidationMessage ret = new DAValidationMessage { PropertyName = propertyName, Message = message };

            ValidationMessages.Add(ret);

            return ret;
        }
        #endregion

        #region Validate Method       
        /// <summary>
        /// Override this method to validate your entity object
        /// </summary>
        /// <param name="entityToValidate">The entity to validate</param>
        /// <returns>True if entity is valid</returns>
        public virtual bool Validate<T>(T entityToValidate)
        {
            string propName = string.Empty;
            ValidationMessages.Clear();

            if (entityToValidate != null)
            {
                ValidationContext context = new ValidationContext(entityToValidate, serviceProvider: null, items: null);
                List<ValidationResult> results = new List<ValidationResult>();

                if (!Validator.TryValidateObject(entityToValidate, context, results, true))
                {
                    foreach (ValidationResult item in results)
                    {
                        if (((string[])item.MemberNames).Length > 0)
                        {
                            propName = ((string[])item.MemberNames)[0];
                        }
                        AddValidationMessage(propName, item.ErrorMessage);
                    }
                }
            }

            return (ValidationMessages.Count > 0);
        }
        #endregion

        #region ThrowDbException Method
        public virtual void ThrowDbException(Exception ex, DbContext db, string exceptionMsg)
        {
            exceptionMsg = string.IsNullOrEmpty(exceptionMsg) ? string.Empty : exceptionMsg + " - ";
            exceptionMsg = exceptionMsg + GetMessageFromException(ex);

            DADbException exc = new DADbException(exceptionMsg, ex)
            {
                ConnectionString = db.Database.GetConnectionString(),
                Database = db.Database.GetDbConnection().Database,
                DataSource = db.Database.GetDbConnection().DataSource,
                SQL = SQL,
                SqlParameters = Parameters,
                WorkstationId = Environment.MachineName
            };

            // Set the last exception
           LastException = exc;

           throw exc;
        }
        #endregion

        #region GetMessageFromException Recursive Method
        private static string GetMessageFromException(Exception ex)
        {
            if (ex.InnerException != null)
                return GetMessageFromException(ex.InnerException);

            return ex.Message;
        }
        #endregion
    }
}
