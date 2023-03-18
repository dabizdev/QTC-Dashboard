using System.ComponentModel.DataAnnotations.Schema;

namespace Common.DA.Library.BaseClasses
{
    public class DAEntityBase
    {
        [NotMapped]
        public int RETURN_VALUE { get; set; }
    }
}
