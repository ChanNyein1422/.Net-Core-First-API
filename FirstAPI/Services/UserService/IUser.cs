using Data.Model;
using Data.ViewModel;
using Infra.Services;

namespace FirstAPI.Services.UserService
{
    public interface IUser
    {
        Task<tbUser> UpSert(tbUser user);
        Task<tbUser> GetByID(int ID);
        Task<bool> Delete(int ID);
        Task<Model<tbUser>> GetList(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                string? q = "", string? email="");
        Task<Model<UserDeptViewModel>> GetUserByDept(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                string? q = "");
        List<tbUser> GetListWithoutPagination();
    }
}
