using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<tbUser> tbUser { get; set; }
        public virtual DbSet<tbDept> tbDept { get; set; }
        public virtual DbSet<tbOrder> tbOrder { get; set; }

    }
}
