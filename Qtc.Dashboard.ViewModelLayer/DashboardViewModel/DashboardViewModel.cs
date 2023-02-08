﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Qtc.Dashboard.BusinessLayer.ApiEntityClasses;
using Qtc.Dashboard.BusinessLayer.AppBaseClasses;
using Qtc.Dashboard.BusinessLayer.Common;
using Qtc.Dashboard.BusinessLayer.EntityClasses;
using Qtc.Dashboard.Common.Contracts;
using Dashboard.Common.Interfaces;
using Dashboard.Common.DataModels;
using Dashboard.Common.Entities;

namespace Qtc.Dashboard.ViewModelLayer.DashboardViewModel
{

    public partial class DashboardViewModel : AppViewModelBase
    {

        private ITenant _tenant;
        public void SetTenant(ITenant tenant)
        {
            _tenant = tenant;
        }

        #region Constructor
        public DashboardViewModel() : base() => Init();

        public DashboardViewModel(
            string lob = "",
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
            SearchEntity = new SearchEntity();
            ViewEntity = new ViewEntity();
            EventAction = "list";
            QtcHeaderImage = Utility.GetQtcHeaderImage;
            DisplayName = "QTC-Dashboard";
        }
        #endregion

        #region Variables
        public ViewEntity ViewEntity { get; set; }
        public SearchEntity SearchEntity { get; set; }
        public AuthUserPermissions Permissions { get; set; }
        public List<RoleMappings> RoleMappings { get; set; }
        public string QtcHeaderImage { get; set; }
        public string? DisplayName { get; set; }
        public List<SearchResultEntity> SearchResults { get; set; } = new List<SearchResultEntity>();


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
                    this.Search();
                    break;
                default:
                    break;
            }
        }

        #endregion

    }

}
