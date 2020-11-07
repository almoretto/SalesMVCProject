using System;

namespace SalesMVC.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException { public DbConcurrencyException(string msg) : base(msg) { } }
}
