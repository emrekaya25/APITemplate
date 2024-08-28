using APITemplate.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.DataAccess.Mapping.BaseMap
{
	public class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.HasKey(x=>x.Id);
			builder.Property(x=>x.Id).ValueGeneratedOnAdd();
		}
	}
}
