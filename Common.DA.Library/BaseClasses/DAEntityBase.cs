using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DA.Library.BaseClasses
{
    public class DAEntityBase
    {
        [NotMapped]
        public int RETURN_VALUE { get; set; }
    }
}
