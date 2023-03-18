using Microsoft.Data.SqlClient;
using System.Text;

namespace Common.DA.Library.Exceptions
{
    internal class DADbException : Exception
    {
        #region Constructors
        public DADbException() : base() { }
        public DADbException(string message) : base(message) { }
        public DADbException(string message, Exception innerException) : base(message, innerException) { }
        #endregion

        #region Properties
        public List<SqlParameter> SqlParameters { get; set; }
        public string SQL { get; set; }
        private string _ConnectionString = string.Empty;
        public string ConnectionString
        {
            get { return HideLoginInfoForConnectionString(_ConnectionString); }
            set { _ConnectionString = value; }
        }
        public string DataSource { get; set; }
        public string Database { get; set; }
        public string WorkstationId { get; set; }
        #endregion

        #region HideLoginInfoForConnectionString Method
        /// <summary>
        /// Looks for UID, User Id, Pwd, Password, etc. in a connection string and replaces their 'values' with astericks.
        /// </summary>
        /// <param name="connectString">The connection string to check</param>
        /// <returns>A string with hidden user id and password values</returns>
        public virtual string HideLoginInfoForConnectionString(string connectString)
        {
            int index = 0;
            string[] values = null;

            connectString = connectString.Trim();
            if (connectString.Length > 0)
            {
                if (!(connectString.EndsWith(";")))
                {
                    connectString += ";";
                }

                values = connectString.Split(';');
                for (index = 0; index <= values.Length - 1; index++)
                {
                    if (values[index].ToLower().IndexOf("uid=") >= 0)
                    {
                        values[index] = "uid=***********";
                    }
                    if (values[index].ToLower().IndexOf("user id=") >= 0)
                    {
                        values[index] = "user id=***********";
                    }
                    if (values[index].ToLower().IndexOf("pwd=") >= 0)
                    {
                        values[index] = "pwd=***********";
                    }
                    if (values[index].ToLower().IndexOf("password=") >= 0)
                    {
                        values[index] = "password=***********";
                    }
                }

                connectString = string.Join(";", values);
            }

            return connectString;
        }
        #endregion

        #region GetCommandParametersAsString Method
        /// <summary>
        /// Gets all parameter names and values from the Command property and returns them all as a CRLF delimited string
        /// </summary>
        /// <returns>A string with all parameter names and values</returns>
        public virtual string GetCommandParametersAsString()
        {
            StringBuilder ret = new StringBuilder(1024);

            if (SqlParameters != null)
            {
                if (SqlParameters.Count > 0)
                {
                    ret = new StringBuilder(1024);

                    foreach (SqlParameter param in SqlParameters)
                    {
                        ret.Append("  " + param.ParameterName);
                        if (param.Value == null)
                            ret.AppendLine(" = null");
                        else
                            ret.AppendLine(" = " + param.Value.ToString());
                    }
                }
            }

            return ret.ToString();
        }
        #endregion

        #region GetDatabaseSpecificError Method
        /// <summary>
        /// Create a string with as much specific database error as possible
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>A string</returns>
        public string GetDatabaseSpecificError(Exception ex)
        {
            SqlException sqlExp = null;
            StringBuilder sb = new StringBuilder(1024);

            if (ex is SqlException)
            {
                sqlExp = ((SqlException)(ex));

                for (int index = 0; index <= sqlExp.Errors.Count - 1; index++)
                {
                    sb.AppendLine(new string('*', 40));
                    sb.AppendLine("**** BEGIN: SQL Server Exception #" + (index + 1).ToString() + " ****");
                    sb.AppendLine("    Type: " + sqlExp.Errors[index].GetType().FullName);
                    sb.AppendLine("    Message: " + sqlExp.Errors[index].Message);
                    sb.AppendLine("    Source: " + sqlExp.Errors[index].Source);
                    sb.AppendLine("    Number: " + sqlExp.Errors[index].Number.ToString());
                    sb.AppendLine("    State: " + sqlExp.Errors[index].State.ToString());
                    sb.AppendLine("    Class: " + sqlExp.Errors[index].Class.ToString());
                    sb.AppendLine("    Server: " + sqlExp.Errors[index].Server);
                    sb.AppendLine("    Procedure: " + sqlExp.Errors[index].Procedure);
                    sb.AppendLine("    LineNumber: " + sqlExp.Errors[index].LineNumber.ToString());
                    sb.AppendLine("**** END: SQL Server Exception #" + (index + 1).ToString() + " ****");
                    sb.AppendLine(new string('*', 40));
                }
            }
            else
            {
                sb.Append(ex.Message);
            }

            return sb.ToString();
        }
        #endregion

        #region IsDatabaseSpecificError Method
        public bool IsDatabaseSpecificError(Exception ex)
        {
            return (ex is SqlException);
        }
        #endregion

        #region GetInnerExceptionInfo Method
        public virtual string GetInnerExceptionInfo()
        {
            StringBuilder sb = new StringBuilder(1024);
            Exception exInner = null;
            int index = 1;

            exInner = InnerException;
            while (exInner != null)
            {
                if (IsDatabaseSpecificError(exInner))
                {
                    sb.Append(GetDatabaseSpecificError(exInner));
                }
                else
                {
                    sb.AppendLine(new string('*', 40));
                    sb.AppendLine("**** BEGIN: Inner Exception #" + index.ToString() + " ****");
                    sb.AppendLine("    Type: " + exInner.GetType().FullName);
                    sb.AppendLine("    Message: " + exInner.Message);
                    sb.AppendLine("    Source: " + exInner.Source);
                    sb.AppendLine("    Trace: " + exInner.StackTrace);
                    sb.AppendLine("**** END: Inner Exception #" + index.ToString() + "****");
                    sb.AppendLine(new string('*', 40));
                }
                index++;

                // Get next inner exception
                exInner = exInner.InnerException;
            }

            return sb.ToString();
        }
        #endregion

        #region Override of ToString Method
        /// <summary>
        /// Gathers all information from the exception information gathered and returns a string
        /// </summary>
        /// <returns>A database specific error string</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(1024);

            sb.AppendLine(new string('-', 80));
            if (!string.IsNullOrEmpty(Message))
            {
                sb.AppendLine("Type: " + this.GetType().FullName);
            }
            if (!string.IsNullOrEmpty(Message))
            {
                sb.AppendLine("Message: " + Message);
            }
            if (!string.IsNullOrEmpty(Database))
            {
                sb.AppendLine("Database: " + Database);
            }
            if (!string.IsNullOrEmpty(Database))
            {
                sb.AppendLine("DataSource: " + DataSource);
            }
            if (!string.IsNullOrEmpty(SQL))
            {
                sb.AppendLine("SQL: " + SQL);
            }
            if (SqlParameters.Count > 0)
            {
                sb.AppendLine("Parameters:");
                sb.Append(GetCommandParametersAsString());
            }
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                sb.AppendLine("Connection String: " + ConnectionString);
            }
            if (!string.IsNullOrEmpty(StackTrace))
            {
                sb.AppendLine("Stack Trace: " + StackTrace);
            }
            if (IsDatabaseSpecificError(this))
            {
                sb.AppendLine(GetDatabaseSpecificError(this));
            }
            // Gather info from inner exceptions
            sb.AppendLine(GetInnerExceptionInfo());

            return sb.ToString();
        }
        #endregion
    }
}
