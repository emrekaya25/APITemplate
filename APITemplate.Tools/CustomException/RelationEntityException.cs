using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Tools.CustomException
{
	public class RelationEntityException:Exception
	{
        public RelationEntityException(List<string> messageList)
        {
            this.Data["RelationEntityException"] = messageList;
        }
    }
}
