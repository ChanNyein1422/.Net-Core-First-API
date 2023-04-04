using Data.Model;
using Data.ViewModel;
using Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper.OrderApiRequest
{
    public interface IOrderApiRequest
    {
        Task<tbOrder> Upsert(tbOrder order);

        Task<tbOrder> GetByID(int id);

        Task<bool> Delete(int id);

        Task<PagedListClient<tbOrder>> getOrder(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                 string? q = "");

        Task<PagedListClient<UserOrderViewModel>> getOrderByUser(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                 string? q = "", string? detail= "");

        Task<PagedListClient<SeparatedUserViewModel>> GetOrderBySeparatedUser(int? page = 1, int? pageSize = 10);

    }
}
