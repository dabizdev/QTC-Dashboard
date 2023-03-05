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
        public bool Load()
        {
            //OrganizationDocumentsManager organizationDocumentsManager = new
            //    OrganizationDocumentsManager();

            //ViewEntity.Organization = organizationDocumentsManager.GetOrganizationByLob(this.Lob);

            var data = _tenant.GetData();

            //if (ViewEntity.Organization != null)
            //{
            //    ViewEntity.Organization.OrganizationDocuments = organizationDocumentsManager.GetOrganizationDocumentsByOrganizationId(ViewEntity.Organization.OrganizationId);
            //    var components = organizationDocumentsManager.GetComponentsByOrganizationId(ViewEntity.Organization.OrganizationId);

            //    for (int i = 0; i < ViewEntity.Organization.OrganizationDocuments.Count; i++)
            //    {
            //        var organizationDocument = ViewEntity.Organization.OrganizationDocuments[i];
            //        ViewEntity.Organization.OrganizationDocuments[i].Components = components.Where(x => x.OrganizationDocumentId == organizationDocument.OrganizationDocumentId).Select(x => x).ToList();

            //    }
            //}

            LoadStaticContent();


            return true;
        }


        public bool LoadStaticContent()
        {

            return true;
        }
    }
}
