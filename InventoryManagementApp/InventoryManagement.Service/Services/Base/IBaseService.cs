using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.Base
{
    public interface IBaseService<T> where T : class
    {
        Task<Paging<T>> GetPageAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize);
        Task<List<T>> GetAllAsync();
        Task<T> FindAsync(long Id);
        Task<T> FirstOrDefaultAsync(long id);
        Task<T> InsertAsync(T entity);
        Task<List<T>> InsertRangeAsync(List<T> entities);
        Task<T> UpdateAsync(long id, T entity);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> UpdateRangeAsync(List<T> entities);
        Task DeleteAsync(long id);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entities);
    }
}
