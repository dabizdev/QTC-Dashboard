using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.EF.Library.Exceptions;

namespace Common.EF.Library.BaseClasses
{
    public class EFViewModelBase : EFCommonBase
    {
        #region Public Properties
        public List<EFValidationMessage> ValidationMessages { get; set; }
        public bool IsValidationVisible { get; set; }
        #endregion

        #region Clear Method
        public virtual void Clear()
        {
            ValidationMessages = new List<EFValidationMessage>();//ValidationMessages.Clear();
            IsValidationVisible = false;
            LastExceptionMessage = string.Empty;
            LastException = null;
            RowsAffected = 0;
        }
        #endregion

        #region DisplayStatusMessage Method
        public virtual void DisplayStatusMessage(string msg = "")
        {
        }
        #endregion

        #region Validate Method
        public virtual bool Validate()
        {
            return true;
        }
        #endregion

        #region ValidationFailed Method
        public virtual void ValidationFailed(EFValidationException ex)
        {
            ValidationMessages = new List<EFValidationMessage>(ex.ValidationMessages);
            IsValidationVisible = true;
        }
        #endregion

        #region PublishException Method
        public void PublishException(Exception ex)
        {
            LastException = ex;
            LastExceptionMessage = ex.ToString();

            // Publish Exception
            EFExceptionManager.Instance.Publish(ex);
        }
        #endregion

        #region Close Method
        public virtual void Close(bool wasCancelled = true)
        {
        }
        #endregion

        protected void Init()
        {
            // Initialize base class properties
            base.Init();
        }
    }
}
