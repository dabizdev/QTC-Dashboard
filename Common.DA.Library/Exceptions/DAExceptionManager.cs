using Common.DA.Library.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DA.Library.Exceptions
{
    public class DAExceptionManager : DACommonBase
    {
        #region Instance Property
        private static DAExceptionManager _Instance;

        public static DAExceptionManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAExceptionManager();
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
