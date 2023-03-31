using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EF.Library.Exceptions
{
    public class EFValidationException : Exception
    {
        #region Constructors
        public EFValidationException() : base()
        {
            Init();
        }
        public EFValidationException(string message) : base(message)
        {
            Init();
        }
        public EFValidationException(string message, Exception innerException) : base(message, innerException)
        {
            Init();
        }
        public EFValidationException(List<EFValidationMessage> messages) : base()
        {
            Init();
            ValidationMessages = messages;
        }
        #endregion

        #region Properties
        public List<EFValidationMessage> ValidationMessages { get; set; }
        #endregion

        #region Init Method
        protected virtual void Init()
        {
            ValidationMessages = new List<EFValidationMessage>();
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

            foreach (EFValidationMessage item in ValidationMessages)
            {
                sb.AppendLine("Property Name: " + item.PropertyName + " - Message: " + item.Message);
            }

            return sb.ToString();
        }
        #endregion
    }
}
