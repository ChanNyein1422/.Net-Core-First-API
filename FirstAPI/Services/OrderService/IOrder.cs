using Data.Model;
using Data.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Infra.Services;

namespace FirstAPI.Services.OrderService
{
    public interface IOrder
    {
        Task<tbOrder> UpSert(tbOrder order);

        Task<Model<tbOrder>> GetOrder(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc", string? q = "");
        Task<tbOrder> GetOrderByID(int ID);

        Task<Model<SeparatedUserViewModel>> GetOrderBySeparatedUser(int? page=1, int? pageSize=10);
        Task<bool> Delete(int ID);
        Task<Model<UserOrderViewModel>> GetOrderByUser(int? page=1, int? pageSize= 10, string? sortVal="ID", string? sortDir="asc", string? q = "", string? detail = "");
    }
}
