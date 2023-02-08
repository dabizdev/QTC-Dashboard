using Common.EF.Library;
using System.Collections.Generic;
using System;

namespace Qtc.Dashboard.BusinessLayer.AppBaseClasses
{
    /// <summary>
    /// This class is the base class for all view models for this  specific application
    /// </summary>
    public class AppViewModelBase : EFViewModelBase
    {
        public string Lob { get; set; }
        public string EventAction { get; set; }
        public string EventValue { get; set; }
        public List<string> Messages { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public bool? ShowSpinner { get; set; }

        protected void Init()
        {
            // Initialize base class properties
            base.Init();
            Message = String.Empty;
            Messages = new List<string>();
        }
    }
}
