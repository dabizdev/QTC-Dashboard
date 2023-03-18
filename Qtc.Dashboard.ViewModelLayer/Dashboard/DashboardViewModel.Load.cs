using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Common.Interfaces;
using Qtc.Dashboard.BusinessLayer.AppBaseClasses;
using Qtc.Dashboard.BusinessLayer.ManagerClasses;
using Qtc.Dashboard.BusinessLayer.EntityClasses;

namespace Qtc.Dashboard.ViewModelLayer.Dashboard
{
    public partial class DashboardViewModel: AppViewModelBase
    {
        public bool Load()
        {
            OrganizationErrorsManager organizationErrorsManager = new
                OrganizationErrorsManager();

            //ViewEntity = new ViewEntity();


            ViewEntity.Organization = organizationErrorsManager.GetOrganizationByLob(this.Lob);
            Console.WriteLine(ViewEntity.Organization);
            ViewEntity.Integrations = organizationErrorsManager.GetOrganizationIntegrationsByOrganizationId(ViewEntity.Organization.OrganizationId);

            // this was the original line that was uncommented
            //var data = _tenant.GetData("SQL");

            Console.WriteLine(ViewEntity.Organization);
            Console.WriteLine(ViewEntity.Integrations);

            // loop through each integration point (stored in the Name column of the OrganizationIntegrations table), 
            // and use the _tenant.GetData() method to retrieve data for each integration point

            foreach (var integration in ViewEntity.Integrations)
            {
                string integrationName = integration.Name;

                var data = _tenant.GetErrorTypeProcessor();
                // Do something with the data
                Console.WriteLine(data);
            }


            //for (int i = 0; i < ViewEntity.Organization.OrganizationIntegrations.Count; i++)
            //{
            //var organizationIntegration = ViewEntity.Organization.OrganizationIntegrations[i];
            //ViewEntity.Organization.OrganizationDocuments[i] = (x => x.OrganizationDocumentId == organizationDocument.OrganizationDocumentId).ToList();

            //   }
            //}

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
