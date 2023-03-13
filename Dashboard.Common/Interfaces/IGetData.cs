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
        // interface that is just meant to return a list of errors
        public string Name { get; set; }
        List<Errors> GetData(string integrationPoint);
    }
}
