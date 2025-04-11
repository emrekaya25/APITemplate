using APITemplate.DataAccess.Mapping;
using APITemplate.Entity.Base;
using APITemplate.Entity.Poco;
using APITemplate.Tools.Logger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.DataAccess.Concrete.Context
{
	public class APITemplateContext:DbContext
	{
        public APITemplateContext()
        {
            
        }
        public APITemplateContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> User { get; set; }
		public DbSet<Role> Role { get; set; }
		public DbSet<UserRole> UserRole { get; set; }
		public DbSet<UserActivityLog> UserActivityLog {get; set;}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=AHLTEK-EMREKAYA; Initial Catalog=APITemplateDb; Integrated Security=true; TrustServerCertificate=True");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserMap());
			modelBuilder.ApplyConfiguration(new RoleMap());
			modelBuilder.ApplyConfiguration(new UserRoleMap());

			base.OnModelCreating(modelBuilder);
		}
	}
}
