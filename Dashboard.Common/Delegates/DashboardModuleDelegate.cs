using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Common.Delegates
{
    // a type that represents references to methods with a particular parameter list, returns a boolean
    public delegate bool DashboardModuleDelegate<T>(T e);
}
