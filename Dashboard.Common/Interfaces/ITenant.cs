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
        public string Name { get; }

        IErrorTypeModule GetErrorTypeProcessor();

    }
}
