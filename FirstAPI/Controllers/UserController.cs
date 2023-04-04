using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstAPI.Services.UserService;
using System.ComponentModel.DataAnnotations;

namespace FirstAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        IUser _iuser;
        public UserController(IUser iuser)
        {
            this._iuser = iuser;
        }
        [HttpPost("api/user/upsert")]
        public async Task<IActionResult> UpSert(tbUser user)
        {
            var data = await this._iuser.UpSert(user);
            return Ok(data);
        }
        [HttpGet("api/user/getbyid")]
        public async Task<IActionResult> GetByID(int ID)
        {
            var data = await this._iuser.GetByID(ID);
            return Ok(data);
        }
        [HttpGet("api/user/delete")]
        public async Task<IActionResult> Delete(int ID)
        {
            var data = await this._iuser.Delete(ID);
            return Ok(data);
        }
        [HttpGet("api/user/getlist")]
        public async Task<IActionResult> GetList(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                string? q = "", string? email="")
        {
            var data = await this._iuser.GetList(page, pageSize, sortVal, sortDir, q, email);
            return Ok(data);
        }
        [HttpGet("api/user/getuserDept")]
        public async Task<IActionResult> GetUserByDept(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                        string? q = "")
        {
            var data = await this._iuser.GetUserByDept(page, pageSize, sortVal, sortDir, q);
            return Ok(data);
        }

        [HttpGet("api/user/getlistwopagination")]
        public IActionResult GetListWithoutPagination()
        {
            var data = this._iuser.GetListWithoutPagination();
            return Ok(data);
        }


    }
}
