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
    public class OrderApiRequest : ApiRequestFactory, IOrderApiRequest
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OrderApiRequest(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<bool> Delete(int id)
        {
            string url = $"api/order/delete?ID={id}";
            var model = await GetRequest<bool>(url.route(Request.firstapi));
            return model;
        }

        public async Task<tbOrder> GetByID(int id)
        {
            string url = $"api/order/getbyid?ID={id}";
            var model = await GetRequest<tbOrder>(url.route(Request.firstapi));
            return model;
        }

        public async Task<PagedListClient<tbOrder>> getOrder(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc", string? q = "")
        {
            string url = $"api/order/getorder?page={page}&pageSize={pageSize}&sortVal={sortVal}&sortDir={sortDir}&q={q}";
            var data = await GetRequest<Model<tbOrder>>(url.route(Request.firstapi));
            PagedListClient<tbOrder> model = PagingService<tbOrder>.Convert(page ?? 1, pageSize ?? 10, data);
            return model;
        }

        public async Task<PagedListClient<UserOrderViewModel>> getOrderByUser(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc", string? q = "", string? detail = "")
        {
            string url = $"api/order/getorderbyuser?page={page}&pageSize={pageSize}&sortVal={sortVal}&sortDir={sortDir}&q={q}&detail={detail}";
            var data = await GetRequest<Model<UserOrderViewModel>>(url.route(Request.firstapi));
            PagedListClient<UserOrderViewModel> model = PagingService<UserOrderViewModel>.Convert(page ?? 1, pageSize ?? 10, data);
            return model;
        }
        public async Task<PagedListClient<SeparatedUserViewModel>> GetOrderBySeparatedUser(int? page = 1, int? pageSize = 10)
        {
            string url = $"api/order/getbyseparateduser?page={page}&pageSize={pageSize}";
            var data = await GetRequest<Model<SeparatedUserViewModel>>(url.route(Request.firstapi));
            PagedListClient<SeparatedUserViewModel> model = PagingService<SeparatedUserViewModel>.Convert(page ?? 1, pageSize ?? 10, data);
            return model;
        }

        public async Task<tbOrder> Upsert(tbOrder order)
        {
            string url = $"api/order/upsert";
            var model = await PostRequest<tbOrder>(url.route(Request.firstapi), order);
            return model;
        }


    }
}
