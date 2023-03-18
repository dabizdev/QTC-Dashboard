using Common.DA.Library.Exceptions;

namespace Common.DA.Library.BaseClasses
{
    public class DAViewModelBase : DACommonBase
    {
        #region Public Properties
        public List<DAValidationMessage> ValidationMessages { get; set; }
        public bool IsValidationVisible { get; set; }
        #endregion

        #region Clear Method
        public virtual void Clear()
        {
            ValidationMessages = new List<DAValidationMessage>();//ValidationMessages.Clear();
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
        public virtual void ValidationFailed(DAValidationException ex)
        {
            ValidationMessages = new List<DAValidationMessage>(ex.ValidationMessages);
            IsValidationVisible = true;
        }
        #endregion

        #region PublishException Method
        public void PublishException(Exception ex)
        {
            LastException = ex;
            LastExceptionMessage = ex.ToString();

            // Publish Exception
            DAExceptionManager.Instance.Publish(ex);
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
