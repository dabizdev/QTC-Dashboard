using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Common.Modules
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DashboardModuleAttribute : Attribute
    {
        public string Name { get; set; }
        public string Lob { get; set; }
    }
}
