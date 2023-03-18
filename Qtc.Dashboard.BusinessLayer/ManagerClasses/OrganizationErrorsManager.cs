using Qtc.Dashboard.BusinessLayer.AppBaseClasses;
using Qtc.Dashboard.BusinessLayer.EntityClasses;

namespace Qtc.Dashboard.BusinessLayer.ManagerClasses
{
    public class OrganizationErrorsManager : AppDataManager
    {
        #region Variables

        public static string GetOrganizationsByLobSp = "[dbo].[GetOrganizationsByLob] @Lob";
        public static string GetOrganizationIntegrationsByOrganizationIdSp = "[dbo].[GetOrganizationIntegrationsByOrganizationId] @OrganizationId";
        //public static string GetComponentsByOrganizationIdSp = "[EFM].[GetComponentsByOrganizationId] @OrganizationId";
        //public static string GetGetOrganizationRolesSp = "[EFM].[GetOrganizationRoles]";
        #endregion

        public Organization GetOrganizationByLob(string lob)
        {
            Organization organization = new Organization();
            List<OrganizationIntegration> Integrations;
            //List<Component> components;
            //List<RoleMappings> roleMappings;

            Init();

            //Create SQL to call stored procedure GetServiceMemberById
            SQL = GetOrganizationsByLobSp;

            // Create parameters
            AddParameter("Lob", (object)lob ?? DBNull.Value, false);

            organization = ExecuteSqlQuery<Organization>("Exception in OrganizationErrorsManager.GetOrganizationByLob()").FirstOrDefault();

            return organization;
        }


        public List<OrganizationIntegration> GetOrganizationIntegrationsByOrganizationId(long organizationId)
        {
            List<OrganizationIntegration> Integrations = new List<OrganizationIntegration>();

            Init();

            //Create SQL to call stored procedure GetServiceMemberById
            SQL = GetOrganizationIntegrationsByOrganizationIdSp;

            // Create parameters
            AddParameter("OrganizationId", (object)organizationId ?? DBNull.Value, false);

            Integrations = ExecuteSqlQuery<OrganizationIntegration>("Exception in OrganizationIntegrationsManager.GetOrganizationIntegrationsByOrganizationId()").ToList();

            return Integrations;
        }


    }
}
