using Data.Model;
using Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private IRepository<tbUser>? _userRepo;
        private IRepository<tbDept>? _deptRepo;
        private IRepository<tbOrder>? _orderRepo;

        private ApplicationDbContext _ctx;
        private bool m_IsDisposed;
        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        ~UnitOfWork()
        {
            _ctx.Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!m_IsDisposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
                _ctx = null;
                m_IsDisposed = true;
            }
        }

        public IRepository<tbUser> userRepo
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new Repository<tbUser>(_ctx);
                }
                return _userRepo;
            }
        }

        public IRepository<tbDept> deptRepo
        {
            get
            {
                if (_deptRepo == null)
                {
                   _deptRepo= new Repository<tbDept>(_ctx);
                }
                return _deptRepo;
            }
        }
        public IRepository<tbOrder> orderRepo
        {
            get
            {
                if (_orderRepo == null)
                {
                    _orderRepo = new Repository<tbOrder>(_ctx);
                }
                return _orderRepo;
            }
        }


    }
}
