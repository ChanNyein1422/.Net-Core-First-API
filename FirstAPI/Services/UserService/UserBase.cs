using Core.Extensions;
using Data.Model;
using Infra.Services;
using Infra.UnitOfWork;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Data.ViewModel;

namespace FirstAPI.Services.UserService
{
    public class UserBase : IUser
    {
        private ApplicationDbContext _context;

        UnitOfWork _uow;
        DateTime now;

        public UserBase(ApplicationDbContext context)
        {
            _context = context;

            _uow = new UnitOfWork(_context);
            now = MyExtension.getLocalTime();
        }

        public async Task<tbUser> UpSert(tbUser user)
        {
            user.Accesstime = now;
            if (user.ID == 0)
            {
                user = await _uow.userRepo.InsertReturnAsync(user);

            }
            else
            {
                user = await _uow.userRepo.UpdateAsync(user);
            }
            return user;
        }

        public async Task<tbUser> GetByID(int ID)
        {
            tbUser user = await _uow.userRepo.GetAll().FirstOrDefaultAsync(a => a.ID == ID && a.IsDeleted != true) ?? new tbUser();
            return user;
        }

        public async Task<bool> Delete(int ID)
        {
            tbUser user = await GetByID(ID);
            user.IsDeleted = true;
            user = await _uow.userRepo.UpdateAsync(user);

            return user != null ? true : false;
        }
        public async Task<Model<tbUser>> GetList(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                string? q = "", string? email = "")
        {
            Expression<Func<tbUser, bool>> basicFilter = null;
            Expression<Func<tbUser, bool>> emailFilter = null;
            IQueryable<tbUser> query = _uow.userRepo.GetAll()
                                            .Where(a => a.IsDeleted != true).AsQueryable();

            if (!String.IsNullOrEmpty(q))
            {
                basicFilter = a => a.Username.Contains(q);
                query = query.Where(basicFilter);
            }

            if (!String.IsNullOrEmpty(email))
            {
                emailFilter = a => a.Email.Contains(email);
                query = query.Where(emailFilter);
            }

            query = SORTLIT<tbUser>.Sort(query, sortVal, sortDir);
            var result = await PagingService<tbUser>.getPaging(page ?? 1, pageSize ?? 10, query);
            return result;
        }

        public async Task<Model<UserDeptViewModel>> GetUserByDept(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc",
                                string? q = "")
        {
            Expression<Func<tbUser, bool>> basicFilter = null;
            IQueryable<tbUser> query = _uow.userRepo.GetAll()
                                            .Where(a => a.IsDeleted != true).AsQueryable();

            if (!String.IsNullOrEmpty(q))
            {
                basicFilter = a => a.Username.Contains(q);
                query = query.Where(basicFilter);
            }

            IQueryable<UserDeptViewModel> result = from u in query
                                                   join d in _uow.deptRepo.GetAll()
                                                   on u.DeptID equals d.ID
                                                  
                                                   select new UserDeptViewModel {
                                                       dept = d, 
                                                       user= u,
                                                       deptname = d.Title
                                                   };

            result = result.OrderBy(a => a.user.ID);
           // query = SORTLIT<tbUser>.Sort(query, sortVal, sortDir);
            var model = await PagingService<UserDeptViewModel>.getPaging(page ?? 1, pageSize ?? 10, result);
            return model;
        }

        public List<tbUser> GetListWithoutPagination()
        {
            var query = _uow.userRepo.GetAll().ToList();
            return query;
        }
    }
}
