using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Common.DataModels;

namespace Dashboard.Common.Interfaces
{
    public interface IGetData
    {
        List<Errors> GetData();
    }
}
