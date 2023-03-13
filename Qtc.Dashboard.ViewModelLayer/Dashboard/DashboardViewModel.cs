using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Common.Interfaces;
using Qtc.Dashboard.BusinessLayer.AppBaseClasses;

namespace Qtc.Dashboard.ViewModelLayer.Dashboard
{
    public partial class DashboardViewModel: AppViewModelBase
    {
        private ITenant _tenant;
        public void SetTenant(ITenant tenant)
        {
            _tenant = tenant;
        }

        #region Constructor
        public DashboardViewModel() : base() => Init();

        public DashboardViewModel(
            string lob = "rhrp",
            string eventAction = "list") : base()
        {
            Init();
            Lob = lob;
            EventAction = eventAction;
        }

        #endregion

        #region Init Method
        /// <summary>
        /// Initialize all public/private properties
        /// </summary>
        protected void Init()
        {
            // Initialize base class properties
            base.Init();
            //SearchEntity = new SearchEntity();
            //ViewEntity = new ViewEntity();
            EventAction = "list";
            //QtcHeaderImage = Utility.GetQtcHeaderImage;
            DisplayName = "Dashboard";
            //Lob = "Another Example ";
        }
        #endregion

        #region Variables
        //public ViewEntity ViewEntity { get; set; }
        //public SearchEntity SearchEntity { get; set; }
        //public AuthUserPermissions Permissions { get; set; }
        //public List<RoleMappings> RoleMappings { get; set; }
        public string QtcHeaderImage { get; set; }
        public string? DisplayName { get; set; }
        //public List<SearchResultEntity> SearchResults { get; set; } = new List<SearchResultEntity>();


        #endregion

        #region HandleRequest Method
        public void HandleRequest()
        {
            // Make sure we have a valid event command
            EventAction = EventAction == null ? string.Empty : EventAction.ToLower();

            switch (EventAction.ToLower())
            {
                case "list":
                    this.Load();
                    break;
                case "search":
                    //this.Search();
                    break;
                default:
                    // TODO: delete this after, just used to test
                    this.Load();
                    break;
            }
        }

        #endregion
    }
}
