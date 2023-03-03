﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DA.Library.Exceptions
{
    public class DAValidationException : Exception
    {
        #region Constructors
        public DAValidationException() : base()
        {
            Init();
        }
        public DAValidationException(string message) : base(message)
        {
            Init();
        }
        public DAValidationException(string message, Exception innerException) : base(message, innerException)
        {
            Init();
        }
        public DAValidationException(List<DAValidationMessage> messages) : base()
        {
            Init();
            ValidationMessages = messages;
        }
        #endregion

        #region Properties
        public List<DAValidationMessage> ValidationMessages { get; set; }
        #endregion

        #region Init Method
        protected virtual void Init()
        {
            ValidationMessages = new List<DAValidationMessage>();
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

            foreach (DAValidationMessage item in ValidationMessages)
            {
                sb.AppendLine("Property Name: " + item.PropertyName + " - Message: " + item.Message);
            }

            return sb.ToString();
        }
        #endregion
    }
}