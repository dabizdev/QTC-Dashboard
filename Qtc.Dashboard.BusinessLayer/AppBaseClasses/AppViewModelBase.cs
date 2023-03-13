using Common.DA.Library;
using Common.DA.Library.BaseClasses;
using Dashboard.Common.DataModels;

namespace Qtc.Dashboard.BusinessLayer.AppBaseClasses
{
    public class AppViewModelBase : DAViewModelBase
    {
        public string Lob { get; set; }
        public string EventAction { get; set; }
        public List<string> IntegrationPoints { get; set; }
        public List<string> Errors { get; set; }
        //public string Message { get; set; }
        //public string User { get; set; }
        public bool? ShowSpinner { get; set; }

        protected void Init()
        {
            // Initialize base class properties
            base.Init();
            //Message = String.Empty;
            //Messages = new List<string>();
            IntegrationPoints = new List<string>();
            Errors = new List<string>();
            Lob = ""; // TODO: DELETE THIS AFTER
        }
    }
}
