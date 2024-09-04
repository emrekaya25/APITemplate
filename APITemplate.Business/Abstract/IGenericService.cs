using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Abstract
{
	public interface IGenericService<TRequest,TResponse>
	{
		public Task<TResponse> AddAsync(TRequest entity);
		public Task<TResponse> UpdateAsync(TRequest entity);
		public Task<TResponse> DeleteAsync(TRequest entity);
		public Task<TResponse> GetAsync(TRequest entity);
		public Task<List<TResponse>> GetAllAsync(TRequest entity);
	}
}
