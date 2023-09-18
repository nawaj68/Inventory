namespace InventoryManagement.Core.Helpers.Excels
{
    public class FileResponseMessage
    {
        public object Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

        public object File { get; set; }
        public string Filename { get; set; }
    }
}
