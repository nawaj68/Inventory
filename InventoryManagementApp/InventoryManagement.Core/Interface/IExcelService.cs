using System.Collections.Generic;
using System.Data;
using System.IO;

namespace InventoryManagement.Core.Interface
{
    public interface IExcelService
    {
        void Save<T>(FileInfo fileInfo, List<T> dataList);
        void Save(FileInfo fileInfo, DataTable dataTable);
    }
}
