using Data.Model;
using Data.ViewModel;
using Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper.UserApiRequest
{
    public interface IUserApiRequest
    {
        Task<tbUser> UpSert(tbUser feedback);
        Task<tbUser> GetByID(int ID);
        Task<bool> Delete(int ID);
        Task<PagedListClient<tbUser>> getlist(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                 string? q = "");
        Task<PagedListClient<UserDeptViewModel>> getlistByDept(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                string? q = "");

        Task<List<tbUser>> GetListWithoutPagination();
    }
}
