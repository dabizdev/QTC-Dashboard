using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Common.DataModels
{
    public class Errors
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public string Layer { get; set; }
        public string Module { get; set; }
        public string Alert { get; set; }
        public string AlertTeam { get; set; }
        public string Severity { get; set; }
        public string ServerName { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ErrorDate { get; set; }
        public string User { get; set; }

    }
}
