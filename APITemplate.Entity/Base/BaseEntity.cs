using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Entity.Base
{
	public class BaseEntity
	{
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public DateTime AddedTime { get; set; }
        public string? AddedIPV4Address { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string? UpdatedIPV4Address { get; set; }
        public Boolean IsActive { get; set; }
    }
}
