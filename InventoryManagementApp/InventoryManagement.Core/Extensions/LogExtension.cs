using Serilog;

namespace InventoryManagement.Core.Extensions
{
    public static class LogExtension
    {
        public static void Warning(string message) => Log.Warning(message);
        public static void Error(string message) => Log.Error(message);
        public static void Information(string message) => Log.Information(message);
        public static void Debug(string message) => Log.Debug(message);

    }
}
