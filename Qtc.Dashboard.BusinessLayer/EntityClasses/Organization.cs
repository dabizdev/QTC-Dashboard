using Common.EF.Library.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qtc.Dashboard.BusinessLayer.EntityClasses
{
    [Table("[dbo].[Organization]")]
    public class Organization : EFEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrganizationId { get; set; }
        public long ParentOrganizationId { get; set; }
        public string? Lob { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Alias { get; set; }
        public string? OrgCode { get; set; }
        [NotMapped]
        public List<OrganizationIntegration> OrganizationIntegrations { get; set; }
    }
}
