using Microsoft.AspNetCore.Identity;
using Shipping.DTO.MerchantDTOs;
using Shipping.Models;

namespace Shipping.Repository.MerchantRepository
{
    public interface IMerchantRepository
    {
        
            Task<List<Merchant>> GetAllMerchantsAsync();
            Task<Merchant> GetMerchantByIdAsync(int id);
            Task<Merchant> AddMerchantAsync(Merchant merchant);
            Task<Merchant> UpdateMerchantAsync(Merchant merchant);
            Task DeleteMerchantAsync(int id);
            Task<List<Merchant>> SearchMerchantsAsync(string query); // Add this line
       
    }
}
