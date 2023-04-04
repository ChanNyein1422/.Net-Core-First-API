using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class UserDeptViewModel
    {
        public tbUser? user { get; set; }
        public tbDept? dept { get; set; }
        public string? deptname { get; set; }
    }
}
