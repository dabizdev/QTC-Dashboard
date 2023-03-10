using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Common.Interfaces;
using Qtc.Dashboard.BusinessLayer.AppBaseClasses;
using Microsoft.Extensions.DependencyInjection;
using Dashboard.Common.DataModels;


namespace Qtc.Dashboard.ViewModelLayer.Dashboard
{
    public partial class DashboardViewModel : AppViewModelBase
    {
        private ITenant _tenant;
        private readonly ITenantRequestHandler _tenantRequestHandler;
        


        public void SetTenant(ITenant tenant)
        {
            _tenant = tenant;
        }

        #region Constructor

        public DashboardViewModel() : base()
        {
            this.TenantNames = new List<string>();
            Init();
        }

        public DashboardViewModel(
            ITenantRequestHandler tenantRequestHandler,
            string lob = "",
            string eventAction = "list") : base()
        {
            _tenantRequestHandler = tenantRequestHandler;
            TenantNames = new List<string>();
            Lob = lob;
            EventAction = eventAction;
            Init().Wait();
        }

        #endregion

        #region Init Method

        protected async Task Init() // make Init an asynchronous method
        {
            base.Init();
            EventAction = "list";
            DisplayName = "Dashboard";
            if (_tenantRequestHandler != null)
            {
                TenantNames = (await _tenantRequestHandler.GetTenantNamesAsync()).ToList(); // use await to get the results of the async method
            }
        }

        #endregion

        #region Variables

        public string QtcHeaderImage { get; set; }
        public string? DisplayName { get; set; }
        public IEnumerable<string> TenantNames { get; set; }

        

        #endregion

        #region HandleRequest Method

        public async void HandleRequest()
        {
            try
            {
                EventAction = EventAction == null ? string.Empty : EventAction.ToLower();

                switch (EventAction.ToLower())
                {
                    case "list":
                        this.Load();
                        break;
                    case "search":
                        //this.Search();
                        break;
                    case "GetTenantNamesAsync":
                        // Do nothing since tenant names were already passed in constructor
                        break;
                    default:
                        this.Load();
                        break;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region GetTenantNames

        // This method is not needed since tenant names are passed in constructor

        #endregion
    }
}