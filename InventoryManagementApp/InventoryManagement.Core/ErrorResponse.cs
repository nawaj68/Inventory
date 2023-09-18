using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core
{
    public class ErrorResponse
    {
        public ErrorResponse() { }

        public ErrorResponse(string message)
        {
            Message = message;
        }

        public ErrorResponse(string message, params object[] args)
        {
            Message = string.Format(message, args);
        }

        public ErrorResponse(IEnumerable<string> messages)
        {
            Message = string.Join(" ", messages);
        }

        public string Message { get; set; }
    }

}
