using Data.Model;
using Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper.DeptApiRequest
{
    public interface IDeptApiRequest
    {
        Task<List<tbDept>> GetDeptList();
    }
}
