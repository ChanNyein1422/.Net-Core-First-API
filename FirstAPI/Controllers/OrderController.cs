using Data.Model;
using FirstAPI.Services.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrder _iorder;

        public OrderController(IOrder iorder)
        {
            this._iorder = iorder;
        }
        [HttpPost("api/order/upsert")]
        public async Task<IActionResult> UpSert(tbOrder order)
        {
            var data = await this._iorder.UpSert(order);
            return Ok(data);
        }
        [HttpGet("api/order/getorder")]
        public async Task<IActionResult> GetOrder(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                string? q = "")
        {
            var data = await this._iorder.GetOrder(page, pageSize, sortVal, sortDir, q);
            return Ok(data);
        }
        [HttpGet("api/order/getorderbyuser")]

        public async Task<IActionResult> GetOrderByUser(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                string? q = "", string? detail = "")
        {
            var data = await this._iorder.GetOrderByUser(page, pageSize, sortVal, sortDir,q, detail);
            return Ok(data);
        }

        [HttpGet("api/order/getbyid")]

        public async Task<IActionResult> GetOrderByID(int ID)
        {
            var data = await this._iorder.GetOrderByID(ID);
            return Ok(data);
        }

        [HttpGet("api/order/getbyseparateduser")]
        public async Task<IActionResult> GetOrderBySeparatedUser()
        {
            var data = await this._iorder.GetOrderBySeparatedUser();
            return Ok(data);
        }
        [HttpGet("api/order/delete")]

        public async Task<IActionResult> Delete(int ID)
        {
            var data = await this._iorder.Delete(ID);
            return Ok(data);
        }
    }
}
