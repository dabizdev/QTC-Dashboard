namespace Qtc.Dashboard.BusinessLayer.EntityClasses
{
    public class ViewEntity
    {
        public Organization Organization { get; set; }
        public List<OrganizationIntegration> Integrations { get; set; }
    }
}
