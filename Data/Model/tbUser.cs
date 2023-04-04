using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class tbUser
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? Accesstime { get; set; }
        public int? Age { get; set;}
        public string? Bio { get; set; }
        public int? DeptID { get; set; }

    }
}
