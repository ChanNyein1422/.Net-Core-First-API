using Data.Model;
using Infra.Helper.UserApiRequest;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVC.Controllers
{
    public class UserController : Controller
    {
        IUserApiRequest _iuser;
        public UserController(IUserApiRequest iuser)
        {
            this._iuser = iuser;
        }
        public IActionResult Index()
        
        {
            return View();
        }

        public async Task<IActionResult> _list(int? page=1, int? pageSize=5, string? sortVal="ID", string? sortDir="asc", string? q = "")
        {
            var data= await this._iuser.getlist(page, pageSize, sortVal, sortDir, q);
            return PartialView("_list", data);
        }

        public async Task<IActionResult> _listByDept(int? page=1, int? pageSize=5, string? sortVal="ID", string? sortDir ="asc", string? q = "") 
        {
            var data = await this._iuser.getlistByDept(page, pageSize, sortVal, sortDir, q);
            return PartialView("_listByDept", data);
        }

        public async Task<IActionResult> list()
        {
            var data = await this._iuser.GetListWithoutPagination();
            return Json(data);
        }

        public async Task<IActionResult> _userForm(string FormType, int ID)
        {
            tbUser user = new tbUser();
            if (FormType == "Add")
            {
                return PartialView("_userForm", user);
            }
            else
            {
                user = await _iuser.GetByID(ID);
                return PartialView("_userForm", user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpSert(tbUser user)
        {
            user = await _iuser.UpSert(user);
            if (user != null)
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }
        }

        public async Task<ActionResult> userdelete(int ID = 0)
        {
            bool result = await _iuser.Delete(ID);
            if (result != false)
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }
        }


    }
}
