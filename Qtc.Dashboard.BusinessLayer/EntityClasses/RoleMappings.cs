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
    public class RoleMappings : EFEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AuthRoleMappingId { get; set; }
        public long OrganizationId { get; set; }
        //public string? OrganizationDocumentType { get; set; }
        public string? AuthRole { get; set; }
    }
}
