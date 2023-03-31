using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.EF.Library.BaseClasses;

namespace Qtc.Dashboard.BusinessLayer.EntityClasses
{
    [Table("[dbo].[OrganizationIntegrations]")]
    public class OrganizationIntegration : EFEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrganizationIntegrationId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Alias { get; set; }

    }
}