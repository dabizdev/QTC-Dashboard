using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * This class is meant to represent a component that is on the sidebar of the dashboard
 */
namespace Dashboard.Common.DataModels
{
    public class OrgsWithApps

    {
        // default constructor
        public OrgsWithApps() { }

        // each organizationWithApps component needs an Organization and list of applications for that organization
        public Organization organization { get; set; }
        public List<Application> applications { get; set; }
    }
}
