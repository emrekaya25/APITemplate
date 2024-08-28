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
	public class UserMap : BaseMap<User>
	{
		public override void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("User");
			builder.Property(x=>x.Email).IsRequired().HasMaxLength(70);
			builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
			builder.Property(x=>x.LastName).IsRequired().HasMaxLength(50);
			builder.Property(x=>x.Password).IsRequired();

			builder.HasData(new User
			{
				Id = 1,
				Name = "Admin",
				LastName = "Admin",
				Image = "string",
				Email = "admin@gmail.com",
				Password = "123",
				AddedTime = DateTime.Now,
				UpdatedTime = DateTime.Now,
				IsActive = true
			});
		}
	}
}
