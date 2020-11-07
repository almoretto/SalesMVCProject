using Microsoft.EntityFrameworkCore;
using SalesMVC.Data;
using SalesMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesMVCContext _context;
        public DepartmentService(SalesMVCContext context) { _context = context; }
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

    }
}
