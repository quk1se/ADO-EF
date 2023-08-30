using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EF.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Entity.Department> Departments { get; set; }
        public DbSet<Entity.Manager> managers { get; set; }

        public DataContext() : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ado-ef-p12;Integrated Security=True");
        }
    }
}
