using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstAPI.Services.UserService;
using FirstAPI.Services.DeptService;

namespace FirstAPI.Controllers
{
    [ApiController]
    public class DeptController : ControllerBase
    {
        IDept _idept;


        public DeptController(IDept idept)
        {
            this._idept = idept;
        }

        [HttpGet("api/user/getDeptList")]
        public IActionResult GetDeptList()
        {
            var data = this._idept.GetDeptList();
            return Ok(data);
        }
    }
}
