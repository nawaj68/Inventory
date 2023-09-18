using System;

namespace InventoryManagement.Core.Exceptions
{
    public class EmptyQueryException : Exception
    {
        public EmptyQueryException(string message) : base(message)
        {
        }
    }
}
