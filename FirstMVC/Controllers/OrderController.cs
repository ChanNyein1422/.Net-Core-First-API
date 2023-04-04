using Data.Model;
using Infra.Helper.OrderApiRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FirstMVC.Controllers
{
    public class OrderController : Controller
    {

        IOrderApiRequest _iorder;

        public OrderController(IOrderApiRequest iorder)
        {
            this._iorder = iorder;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(tbOrder order)
        {
            order = await _iorder.Upsert(order);
            if (order != null) 
            { 
                return Json("Success"); 
            }else
            {
                return Json("Fail");
            }
        }
        public async Task<IActionResult> OrderDelete(int id=0)
        {
            bool result = await _iorder.Delete(id);
            if (result)
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }
        }

        public async Task<IActionResult> _orderlist(int? page = 1, int? pageSize = 5, string? sortVal = "ID", string? sortDir = "asc", string? q = "")
        {
            var data = await this._iorder.getOrder(page, pageSize, sortVal, sortDir, q);
            return PartialView("_orderlist", data);
        }
        public async Task<IActionResult> _orderlistbyuser(int? page = 1, int? pageSize = 5, string? sortVal = "ID", string? sortDir = "asc", string? q = "", string? detail= "")
        {
            var data = await this._iorder.getOrderByUser(page, pageSize, sortVal, sortDir, q, detail);
            return PartialView("_orderlistbyuser", data);

        }

        public async Task<IActionResult> _orderViewByUserSeparated(int? page=1, int? pageSize = 5)
        {
            var data = await this._iorder.GetOrderBySeparatedUser(page, pageSize);
            return PartialView("_orderViewByUserSeparated", data);
        }
        public async Task<IActionResult> _orderform(string Formtype, int id)
        {
            tbOrder order = new tbOrder();
            if(Formtype == "Add")
            {
                return PartialView("_orderform", order);
            }
            else
            {
                order = await _iorder.GetByID(id);
                return PartialView("_orderform", order);
            }
        }
    }
}
