using APITemplate.DataAccess.Mapping.BaseMap;
using APITemplate.Entity.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.DataAccess.Mapping
{
	public class UserRoleMap:BaseMap<UserRole>
	{
		public override void Configure(EntityTypeBuilder<UserRole> builder)
		{
			builder.ToTable("UserRole");
			builder.HasOne(x=>x.User).WithMany(x=>x.UserRoles).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(x=>x.Role).WithMany(x=>x.UserRoles).HasForeignKey(x=>x.RoleId).OnDelete(DeleteBehavior.NoAction);

			builder.HasData(new UserRole
			{
				AddedTime = DateTime.Now,
				UpdatedTime = DateTime.Now,
				IsActive = true,
				RoleId = 1,
				UserId = 1,
				Id = 1
			});
		}
	}
}
