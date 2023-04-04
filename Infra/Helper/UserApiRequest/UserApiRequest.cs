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
    public class UserApiRequest : ApiRequestFactory, IUserApiRequest
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserApiRequest(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<tbUser> UpSert(tbUser user)
        {
            string url = $"api/user/upsert";
            var model = await PostRequest<tbUser>(url.route(Request.firstapi), user);
            return model;
        }

        public async Task<tbUser> GetByID(int ID)
        {
            string url = $"api/user/getbyid?ID={ID}";
            var model = await GetRequest<tbUser>(url.route(Request.firstapi));
            return model;
        }

        public async Task<bool> Delete(int ID)
        {
            string url = $"api/user/delete?ID={ID}";
            var model = await GetRequest<bool>(url.route(Request.firstapi));
            return model;
        }

        public async Task<PagedListClient<tbUser>> getlist(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                string? q = "")
        {
            string url = $"api/user/getlist?page={page}&pageSize={pageSize}&sortVal={sortVal}&sortDir={sortDir}&q={q}";
            var data = await GetRequest<Model<tbUser>>(url.route(Request.firstapi));
            PagedListClient<tbUser> model = PagingService<tbUser>.Convert(page ?? 1, pageSize ?? 10, data);
            return model;
        }

        public async Task<PagedListClient<UserDeptViewModel>> getlistByDept(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                               string? q = "")
        {
            string url = $"api/user/getuserDept?page={page}&pageSize={pageSize}&sortVal={sortVal}&sortDir={sortDir}&q={q}";
            var data = await GetRequest<Model<UserDeptViewModel>>(url.route(Request.firstapi));
            PagedListClient<UserDeptViewModel> model = PagingService<UserDeptViewModel>.Convert(page ?? 1, pageSize ?? 10, data);
            return model;
        }

        public async Task<List<tbUser>> GetListWithoutPagination()
        {
            string url = $"api/user/getlistwopagination";
            var data = await GetRequest<List<tbUser>>(url.route(Request.firstapi));
            return data;
        }
    }
}
