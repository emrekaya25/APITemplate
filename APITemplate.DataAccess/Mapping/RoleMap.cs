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
	public class RoleMap:BaseMap<Role>
	{
		public override void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("Role");
			builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

			builder.HasData(new Role
			{
				Id = 1,
				Name = "Admin",
				AddedTime = DateTime.UtcNow,
				UpdatedTime = DateTime.UtcNow,
				IsActive = true,
			},
			new Role
			{
				Id= 2,
				Name = "Çalışan",
				AddedTime = DateTime.UtcNow,
				UpdatedTime = DateTime.UtcNow,
				IsActive = true
			});
		}
	}
}
