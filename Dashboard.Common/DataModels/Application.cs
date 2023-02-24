using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Common.DataModels
{
    public class Application
    {
        public Int64 ApplicationId { get; init; }
        public string ApplicationName { get; init; }
        public Int64 UserId { get; init; }
        public string APIUrl { get; init; }
        public string UserType { get; init; }
        public string CreatedBy { get; init; }
        public DateTime CreatedDate { get; init; }
        public DateTime UpdatedDate { get; init; }
        public string UpdatedBy { get; init; }

    }
}
