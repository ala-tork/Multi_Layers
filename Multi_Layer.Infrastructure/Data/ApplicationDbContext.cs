using Microsoft.EntityFrameworkCore;
using Multi_Layer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Layer.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base( options) { }
        public virtual DbSet<User> Users { get; set; }
    }
}
