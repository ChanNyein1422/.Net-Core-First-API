using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class UserOrderViewModel
    {
        public tbOrder? order { get; set; }
        public tbUser? user { get; set; }
    }
}
