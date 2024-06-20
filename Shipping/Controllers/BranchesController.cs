using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.DTO.BranchDTOs;
using Shipping.Models;
using Shipping.Repository.BranchRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public BranchController(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }


        #region GetAllBranches
        [HttpGet]
        public async Task<ActionResult<List<BranchDTO>>> GetAllBranches()
        {
            var branches = await _branchRepository.GetAllAsync();
            var branchDTOs = _mapper.Map<List<BranchDTO>>(branches);
            return Ok(branchDTOs);
        }
        #endregion

        #region GetBranchById 
        [HttpGet("{id}")]

        public async Task<ActionResult<BranchDTO>> GetBranchById(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);

            if (branch == null)
            {
                return NotFound("الفرع غير موجود");
            }

            var branchDTO = _mapper.Map<BranchDTO>(branch);
            return Ok(branchDTO);
        }
        #endregion


        #region Add New Branch
        [HttpPost]
        public async Task<ActionResult<BranchDTO>> AddBranch(BranchDTO branchDTO)
        {
            var branch = _mapper.Map<Branch>(branchDTO);

            try
            {
                await _branchRepository.AddAsync(branch);

                var createdDto = _mapper.Map<BranchDTO>(branch);
                return CreatedAtAction(nameof(GetBranchById), new { id = createdDto.Id }, $"تمت إضافة الفرع بنجاح. معرف الفرع الجديد: {createdDto.Id}");
            }
            catch (Exception ex)
            {
                return BadRequest($"فشلت العملية: {ex.Message}");
            }
        }
        #endregion

        #region Update Branch
        [HttpPut("{id}")]
    
        public async Task<IActionResult> UpdateBranch(int id, BranchDTO branchDTO)
        {
            if (id != branchDTO.Id)
            {
                return BadRequest("معرف الفرع المحدث غير متطابق مع معرف الفرع في الطلب.");
            }

            var existingBranch = await _branchRepository.GetByIdAsync(id);
            if (existingBranch == null)
            {
                return NotFound("الفرع غير موجود");
            }

            try
            {
                var branch = _mapper.Map(branchDTO, existingBranch);
                await _branchRepository.UpdateAsync(branch);
                return Ok(_mapper.Map<BranchDTO>(branch));
            }
            catch (Exception ex)
            {
                return BadRequest($"فشلت العملية: {ex.Message}");
            }
        }
        #endregion

        #region DeleteBranch

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteBranch(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
            {
                return NotFound("الفرع غير موجود");
            }

            try
            {
                await _branchRepository.DeleteAsync(id);
                return Ok("تم حذف الفرع بنجاح");
            }
            catch (Exception ex)
            {
                return BadRequest($"فشلت العملية: {ex.Message}");
            }
        }
        #endregion
    }
}
