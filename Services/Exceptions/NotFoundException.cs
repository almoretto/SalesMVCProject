using System;

namespace SalesMVC.Services.Exceptions
{
    public class NotFoundException : ApplicationException { public NotFoundException(string msg) : base(msg) { } }
}
