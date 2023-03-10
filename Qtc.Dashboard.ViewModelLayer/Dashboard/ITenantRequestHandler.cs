using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qtc.Dashboard.ViewModelLayer.Dashboard
{
    public interface ITenantRequestHandler
    {
        Task<string[]> GetTenantNamesAsync();
    }
}
