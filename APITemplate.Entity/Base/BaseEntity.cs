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
        public DateTime AddedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public Boolean IsActive { get; set; }
    }
}
