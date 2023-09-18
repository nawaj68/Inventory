using System.Collections.Generic;

namespace InventoryManagement.Common.Collections
{
    public interface IPagedList<T>
    {
        IList<T> Items { get; set; }
        int Total { get; set; }
    }
}
