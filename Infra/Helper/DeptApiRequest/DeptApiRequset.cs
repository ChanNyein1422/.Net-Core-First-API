using Data.Model;
using Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper.DeptApiRequest
{
    public class DeptApiRequest : ApiRequestFactory, IDeptApiRequest
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DeptApiRequest(IHttpClientFactory httpClientFactory) : base(httpClientFactory) 
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<List<tbDept>> GetDeptList()
        {
            string url = $"api/user/getDeptList";
            var data = await GetRequest<List<tbDept>>(url.route(Request.firstapi));
            return data ;
        }
    }
}
