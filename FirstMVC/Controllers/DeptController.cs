using Data.Model;
using Infra.Helper.DeptApiRequest;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVC.Controllers
{
    public class DeptController : Controller
    {
        IDeptApiRequest _idept;
        
        public DeptController(IDeptApiRequest idept)
        {
            this._idept = idept;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> list()
        {
            var data = await this._idept.GetDeptList();
            return Json(data);
        }

    }
}
