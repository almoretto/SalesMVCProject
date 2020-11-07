using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesMVC.Data;
using SalesMVC.Models;
using SalesMVC.Services.Exceptions;


namespace SalesMVC.Services
{
    public class SellerService
    {
        private readonly SalesMVCContext _context;
        [BindProperty]
        public Seller Seller { get; set; }

        public SellerService(SalesMVCContext context) { _context = context; }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller
                .OrderBy(s => s.Name)
                .Include(s => s.Department)
                .ToListAsync();
        }
        public async Task InsertAsync(Seller s)
        {
            _context.Seller.Add(s);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller
                .Include(slr => slr.Department)
                .FirstOrDefaultAsync(slr => slr.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            var slr = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(slr);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Seller slr)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == slr.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Seller: " + slr.Name + " Id: " + slr.Id + " was not found in database!");
            }
            try
            {
                _context.Update(slr);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                {
                    msg = "";
                    msg = ex.Message + "\n" + ex.InnerException.Message;
                }
                throw new DbConcurrencyException(msg);
            }
        }
    }
}
