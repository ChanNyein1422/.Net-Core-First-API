using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class SeparatedUserViewModel
    {
        public tbUser? user {  get; set; }
        public List<tbOrder>? orders { get; set; }
    }
}
