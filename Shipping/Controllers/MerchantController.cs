using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shipping.DTO.MerchantDTOs;
using Shipping.Models;
using Shipping.Repository;
using Shipping.Repository.MerchantRepository;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Shipping.Repository.MerchantRepository.IMerchantRepository;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMapper _mapper;

        public MerchantController(IMerchantRepository merchantRepository, IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _mapper = mapper;
        }

        #region Get All Merchants
        [HttpGet]
        public async Task<IActionResult> GetAllMerchants()
        {
            var merchants = await _merchantRepository.GetAllMerchantsAsync();
            var merchantDTOs = _mapper.Map<List<MerchantDTO>>(merchants);
            return Ok(merchantDTOs);
        }
        #endregion

        #region Add Merchant
        [HttpPost]
        public async Task<IActionResult> AddMerchant(MerchantDTO merchantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("بيانات غير صالحة");
            }

            try
            {
                var merchant = _mapper.Map<Merchant>(merchantDTO);
                var createdMerchant = await _merchantRepository.AddMerchantAsync(merchant);
                var createdMerchantDTO = _mapper.Map<MerchantDTO>(createdMerchant);
                return Ok(createdMerchantDTO);
            }
            catch (Exception ex)
            {
                return BadRequest($"فشلت العملية: {ex.Message}");
            }
        }
        #endregion

        #region Get Merchant by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMerchantById(int id)
        {
            var merchant = await _merchantRepository.GetMerchantByIdAsync(id);
            if (merchant == null)
            {
                return NotFound("التاجر غير موجود");
            }
            var merchantDTO = _mapper.Map<MerchantDTO>(merchant);
            return Ok(merchantDTO);
        }
        #endregion

        #region Update Merchant
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMerchant(int id, MerchantDTO merchantDTO)
        {
            var existingMerchant = await _merchantRepository.GetMerchantByIdAsync(id);
            if (existingMerchant == null)
            {
                return NotFound("التاجر غير موجود");
            }

            try
            {
                _mapper.Map(merchantDTO, existingMerchant);
                var updatedMerchant = await _merchantRepository.UpdateMerchantAsync(existingMerchant);
                var updatedMerchantDTO = _mapper.Map<MerchantDTO>(updatedMerchant);
                return Ok(updatedMerchantDTO);
            }
            catch (Exception ex)
            {
                return BadRequest($"فشلت العملية: {ex.Message}");
            }
        }
        #endregion

        #region Delete Merchant
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMerchant(int id)
        {
            var existingMerchant = await _merchantRepository.GetMerchantByIdAsync(id);
            if (existingMerchant == null)
            {
                return NotFound("التاجر غير موجود");
            }

            try
            {
                await _merchantRepository.DeleteMerchantAsync(id);
                return Ok("تم حذف التاجر بنجاح");
            }
            catch (Exception ex)
            {
                return BadRequest($"فشلت العملية: {ex.Message}");
            }
        }
        #endregion
    }
}
