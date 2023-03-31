using Common.EF.Library.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EF.Library.Exceptions
{
    public class EFExceptionManager : EFCommonBase
    {
        #region Instance Property
        private static EFExceptionManager _Instance;

        public static EFExceptionManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new EFExceptionManager();
                }

                return _Instance;
            }
            set { _Instance = value; }
        }
        #endregion

        #region Publish Methods
        public virtual void Publish(Exception ex)
        {
            // TODO: Implement an exception publisher here
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
        #endregion
    }
}
