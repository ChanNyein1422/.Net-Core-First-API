using Data.Model;
using Data.ViewModel;
using Infra.Services;

namespace FirstAPI.Services.DeptService
{
    public interface IDept
    {
        List<tbDept> GetDeptList();
    }
}
