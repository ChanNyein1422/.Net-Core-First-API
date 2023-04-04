using Core.Extensions;
using Data.Model;
using Infra.Services;
using Infra.UnitOfWork;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Data.ViewModel;
using System.Security.Cryptography.X509Certificates;

namespace FirstAPI.Services.DeptService
{
    public class DeptBase : IDept
    {
        private ApplicationDbContext _context;
        UnitOfWork _uow;
        DateTime now;

        public DeptBase(ApplicationDbContext context)
        {
            _context = context;

            _uow = new UnitOfWork(_context);
            now = MyExtension.getLocalTime();
        }

        public List<tbDept> GetDeptList()
        {          
            var query = _uow.deptRepo.GetAll().ToList();
            return query;

        }
    }
}
