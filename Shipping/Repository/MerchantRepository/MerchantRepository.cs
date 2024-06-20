using Microsoft.EntityFrameworkCore;
using Shipping.Models;
using Shipping.Repository.MerchantRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.Repository.MerchantRepository
{
    public class MerchantRepository:IMerchantRepository
    {
        private readonly ShippingContext _context;

        public MerchantRepository(ShippingContext context)
        {
            _context = context;
        }

        public async Task<List<Merchant>> GetAllMerchantsAsync()
        {
            return await _context.Merchants.ToListAsync();
        }

        public async Task<Merchant> GetMerchantByIdAsync(int id)
        {
            return await _context.Merchants.FindAsync(id);
        }

        public async Task<Merchant> AddMerchantAsync(Merchant merchant)
        {
            _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync();
            return merchant;
        }

        public async Task<Merchant> UpdateMerchantAsync(Merchant merchant)
        {
            _context.Entry(merchant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return merchant;
        }

        public async Task DeleteMerchantAsync(int id)
        {
            var merchant = await _context.Merchants.FindAsync(id);
            if (merchant != null)
            {
                _context.Merchants.Remove(merchant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Merchant>> SearchMerchantsAsync(string query)
        {
            return await _context.Merchants
                .Where(m => m.User.Name.Contains(query) || m.Address.Contains(query) || m.City.Contains(query))
                .ToListAsync();
        }
    }
}
