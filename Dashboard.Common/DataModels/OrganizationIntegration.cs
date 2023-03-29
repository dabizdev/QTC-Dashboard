using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Common.DataModels
{
    public class OrganizationIntegration
    {
        public Int64 OrganizationIntegrationId { get; init; }
        public Int64 OrganizationId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Alias { get; init; }
        public DateTime CreatedDate { get; init; }
        public string CreatedBy { get; init; }
        public DateTime UpdatedDate { get; init; }
        public string UpdatedBy { get; init; }

        /*
        public Int64 UserId { get; init; }
        public string APIUrl { get; init; }
        public string UserType { get; init; }
        */
        
       

    }
}
