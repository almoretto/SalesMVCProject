using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesMVC.Data;
using SalesMVC.Models;

namespace SalesMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesMVCContext _context;

        public SalesRecordService(SalesMVCContext context) { _context = context; }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var salesRecordQuery = from sR in _context.SalesRecord select sR;
            if (minDate.HasValue)
            {
                salesRecordQuery = salesRecordQuery.Where(d => d.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                salesRecordQuery = salesRecordQuery.Where(d => d.Date <= maxDate.Value);
            }

            salesRecordQuery = salesRecordQuery
                .Include(sr => sr.Seller)
                .Include(sr => sr.Seller.Department)
                .OrderByDescending(sr => sr.Date);

            return await salesRecordQuery.ToListAsync();
        }
        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupedAsync(DateTime? minDate, DateTime? maxDate)
        {
            var salesRecordQuery = from sR in _context.SalesRecord select sR;
            if (minDate.HasValue)
            {
                salesRecordQuery = salesRecordQuery.Where(d => d.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                salesRecordQuery = salesRecordQuery.Where(d => d.Date <= maxDate.Value);
            }

            return await salesRecordQuery
                .Include(sr => sr.Seller)
                .Include(sr => sr.Seller.Department)
                .OrderByDescending(sr => sr.Date)
                .GroupBy(sr => sr.Seller.Department).ToListAsync();
        }
    }
}
