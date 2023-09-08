using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstAPI.Services.UserService;
using FirstAPI.Services.DeptService;
using Infra.Helper;


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
        [HttpGet("api/encryption/encrypt")]
        public IActionResult Encrypt(string plaintext)
        {
            var data = CryptoHelper.Encrypt(plaintext);
            return Ok(data);
        }

        [HttpGet("api/encryption/decrypt")]
        public IActionResult Decrypt(string ciphertext)
        {
            var data = CryptoHelper.Decrypt(ciphertext);
            return Ok(data);
        }
    }
}
