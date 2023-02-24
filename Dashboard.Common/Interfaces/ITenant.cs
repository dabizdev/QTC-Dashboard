using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Common.Modules;

namespace Dashboard.Common.Interfaces
{
    public interface ITenant
    {
        // a tenant name can only be retreived 
        public string Name { get; }

        // IErrorTypeModule is an interface that doesn't implement anything yet
        IErrorTypeModule GetErrorTypeProcessor();
    }
}
