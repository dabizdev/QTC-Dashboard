using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EF.Library.BaseClasses
{
    public class EFEntityBase
    {
        [NotMapped]
        public int RETURN_VALUE { get; set; }
    }
}
