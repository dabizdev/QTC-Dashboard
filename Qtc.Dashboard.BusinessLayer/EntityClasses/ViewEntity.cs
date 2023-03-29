using Dashboard.Common.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qtc.Dashboard.BusinessLayer.EntityClasses
{
    public class ViewEntity
    {
        public Organization Organization { get; set; }
        public List<OrganizationIntegration> Integrations { get; set; }
    }
}
