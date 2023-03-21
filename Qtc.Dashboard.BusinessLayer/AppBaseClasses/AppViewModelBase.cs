using Common.DA.Library;
using Common.DA.Library.BaseClasses;
using Dashboard.Common.DataModels;

namespace Qtc.Dashboard.BusinessLayer.AppBaseClasses
{
    public class AppViewModelBase : DAViewModelBase
    {
        public string Lob { get; set; }
        public string EventAction { get; set; }
        //public string EventValue { get; set; }
        //public List<string> Messages { get; set; }
        //public string Message { get; set; }
        //public string User { get; set; }
        public bool? ShowSpinner { get; set; }

        public List<Errors> ListOfErrors { get; set; }

        public List<string> headers { get; set; }

        protected void Init()
        {
            // Initialize base class properties
            base.Init();
            //Message = String.Empty;
            //Messages = new List<string>();
            //Lob = this.Lob; // TODO: DELETE THIS AFTER
        }
    }
}
