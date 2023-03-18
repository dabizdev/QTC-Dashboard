namespace Common.DA.Library.BaseClasses
{
    public class DACommonBase
    {
        #region Constructor
        public DACommonBase()
        {
            Init();
        }
        #endregion

        #region Private Variables
        private Exception _LastException = null;
        #endregion

        #region Public Properties  
        /// <summary>
        /// Get/Set LastException
        /// </summary>
        public Exception LastException
        {
            get { return _LastException; }
            set
            {
                _LastException = value;
                LastExceptionMessage = (value == null ? string.Empty : value.Message);
            }
        }

        /// <summary>
        /// Get/Set LastExceptionMessage
        /// </summary>
        public string LastExceptionMessage { get; set; }

        /// <summary>
        /// Get/Set Rows Affected
        /// </summary>
        public int RowsAffected { get; set; }

        public int Count { get; set; }
        #endregion

        #region Init Method
        public virtual void Init()
        {
            LastException = null;
            LastExceptionMessage = string.Empty;
            RowsAffected = 0;
        }
        #endregion
    }
}
