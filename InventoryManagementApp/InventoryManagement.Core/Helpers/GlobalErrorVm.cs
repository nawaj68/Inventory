namespace InventoryManagement.Core.Helpers
{
    public class GlobalErrorVm
    {
        public string ErrMsg { get; set; }
        public object Detail { get; set; }
        public object StackTrace { get; set; }
    }
}
