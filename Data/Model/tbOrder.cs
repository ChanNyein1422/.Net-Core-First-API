using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class tbOrder
    {
        public int ID { get; set; }
        public int? OrderID { get; set; }
        public DateTime? OrderTime { get; set; }
        public string? OrderDetails { get; set; }
        public int? UserID { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
