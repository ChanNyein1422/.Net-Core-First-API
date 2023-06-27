using Core.Extensions;
using Data.Model;
using Infra.Services;
using Infra.UnitOfWork;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Data.ViewModel;
using FirstAPI.Services.UserService;
using System.Threading.Tasks.Dataflow;
using LinqKit;

namespace FirstAPI.Services.OrderService
{
    public class OrderBase : IOrder
    {
        private ApplicationDbContext _context;
        UnitOfWork _uow;
        DateTime now;

        public OrderBase(ApplicationDbContext context)
        {
            _context = context;

            _uow = new UnitOfWork(_context);
            now = MyExtension.getLocalTime();
        }
        public async Task<bool> Delete(int ID)
        {
            tbOrder order = await GetOrderByID(ID);
            order.IsDeleted = true;
            order = await _uow.orderRepo.UpdateAsync(order);
            return order != null? true: false;
        }
        
        public async Task<Model<UserOrderViewModel>> GetOrderByUser(int? page=1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc", string? q = "", string? detail = "")
        {
            Expression<Func<tbUser, bool>> basicFilter = null;
            Expression<Func<tbOrder, bool>> detailFilter = null;
            IQueryable<tbOrder> query = _uow.orderRepo.GetAll().Where(o => o.IsDeleted !=true).AsQueryable();
            if (!String.IsNullOrEmpty(q))
            {
                basicFilter = PredicateBuilder.New<tbUser>();
                basicFilter = basicFilter.Or(a => a.Username.Contains(q));
                basicFilter = basicFilter.Or(a => a.Email.Contains(q));
                //   query = query.Where(basicFilter);
            }
            else
            {
                basicFilter = o => o.IsDeleted != true;
            }
            if (!String.IsNullOrEmpty(detail))
            {
                detailFilter = o => o.OrderDetails.Contains(detail);
                query = query.Where(detailFilter);
            }
            
            IQueryable<UserOrderViewModel> result = from o in query
                                                    join u in _uow.userRepo.GetAll().Where(basicFilter)
                                                    on o.UserID equals u.ID

                                                    select new UserOrderViewModel
                                                    {
                                                        order = o,
                                                        user = u,
                                                    };
            result = result.OrderBy(o => o.order.ID);
            var model = await PagingService<UserOrderViewModel>.getPaging(page ?? 1, pageSize ?? 10, result);
            return model;
        }
        public async Task<Model<tbOrder>> GetOrder(int? page = 1, int? pageSize = 10, string? sortVal = "ID", string? sortDir = "asc", string? q = "")
        {
            Expression<Func<tbOrder, bool>> basicFilter = null;
            IQueryable<tbOrder> query = _uow.orderRepo.GetAll().Where(o => o.IsDeleted != true).AsQueryable();
            
            if (!String.IsNullOrEmpty(q))
            {
                basicFilter = a => a.OrderDetails.Contains(q);
                query = query.Where(basicFilter);
            }

            query = SORTLIT<tbOrder>.Sort(query, sortVal, sortDir);
            var res = await PagingService<tbOrder>.getPaging(page ?? 1, pageSize ?? 10, query);
            return res;
        }

        public async Task<tbOrder> GetOrderByID(int ID)
        {
            tbOrder order = await _uow.orderRepo.GetAll().FirstOrDefaultAsync(o => o.ID == ID && o.IsDeleted != true) ?? new tbOrder();
            return order;
        }

        //Separated User Order View
        public async Task<Model<SeparatedUserViewModel>> GetOrderBySeparatedUser(int? page = 1, int? pageSize = 10)
        {
            var result = _uow.userRepo.GetAll().Where(a => a.IsDeleted != true)
                                .GroupJoin(_uow.orderRepo.GetAll().Where(o => o.IsDeleted != true),
                                  user => user.ID,
                                  orders => orders.UserID, (user, orders) => new SeparatedUserViewModel
                                  {
                                      user = user,
                                      orders = orders.ToList(),

                                  }).AsQueryable();

            var res = await PagingService<SeparatedUserViewModel>.getPagingList(page ?? 1, pageSize ?? 10, result);
            return res;
        }


        public async Task<tbOrder> UpSert(tbOrder order)
        {
            order.IsDeleted = false;

            order.OrderTime = now;
            if (order.ID == 0)
            {
                order = await _uow.orderRepo.InsertReturnAsync(order);
            }
            else
            {
                order = await _uow.orderRepo.UpdateAsync(order);
            }
            return order;
        }


    }
}
