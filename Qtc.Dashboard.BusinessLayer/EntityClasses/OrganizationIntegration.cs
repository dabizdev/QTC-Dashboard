using Common.DA.Library.BaseClasses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qtc.Dashboard.BusinessLayer.EntityClasses
{
    [Table("[dbo].[OrganizationIntegrations]")]
    public class OrganizationIntegration : DAEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrganizationIntegrationId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Alias { get; set; }

    }
}