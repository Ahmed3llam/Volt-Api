// DeliveryController.cs

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Shipping.Constants;
using Shipping.DTO.DeliveryDTOs;
using Shipping.Models;
using Shipping.UnitOfWork;
using swagger = Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IUnitOfWork<Delivery> unitOfWork;
        private readonly IMapper mapper;

        public DeliveryController(IUnitOfWork<Delivery> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        #region Get All Delivery

        [HttpGet]
        [Authorize(Permissions.Deliveries.View)]
        [swagger.SwaggerOperation(Summary = "Show all Deliveries.")]
        [swagger.SwaggerResponse(StatusCodes.Status201Created, "Deliveries successfully Retrieved.")]
        [swagger.SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data. Please check the provided information.")]
        public async Task<IActionResult> GetAllDeliveries()
        {
            try
            {
                var deliveries = await unitOfWork.DeliveryRepository.GetAllDeliveries();
                var deliveryDTOs = mapper.Map<List<DeliveryDTO>>(deliveries);
                return Ok(deliveryDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve deliveries: {ex.Message}");
            }
        }

        #endregion


        #region Add New Delivery

        // POST: api/Delivery/AddDelivery
        [HttpPost("AddDelivery")]
        [Authorize(Permissions.Deliveries.Create)]
        [swagger.SwaggerOperation(Summary = "Add New Delivery.")]
        [swagger.SwaggerResponse(StatusCodes.Status201Created, "Delivery successfully Created.")]
        [swagger.SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data. Please check the provided information.")]

        public async Task<IActionResult> Add(DeliveryDTO newDeliveryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                //var newDelivery = mapper.Map<Delivery>(newDeliveryDto);
                var addedDelivery = await unitOfWork.DeliveryRepository.Insert(newDeliveryDto);
                var addedDeliveryDto = mapper.Map<DeliveryDTO>(addedDelivery);

                return Ok(addedDeliveryDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add delivery: {ex.Message}");
            }
        }

        #endregion


        #region Edit Delivery

        [HttpPut("EditDelivery/{id}")]
        [Authorize(Permissions.Deliveries.Edit)]
        [swagger.SwaggerOperation(Summary = "Edit Existing Delivery.")]
        [swagger.SwaggerResponse(StatusCodes.Status201Created, "Delivery successfully Updated.")]
        [swagger.SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data. Please check the provided information.")]

        public async Task<IActionResult> EditDelivery(string id, DeliveryDTO updatedDeliveryDto)
        {
            var existingDelivery = await unitOfWork.DeliveryRepository.GetById(id);
            if (existingDelivery == null)
            {
                return NotFound("Delivery not found.");
            }

            mapper.Map(updatedDeliveryDto, existingDelivery);
            var editedDelivery = await unitOfWork.DeliveryRepository.EditDelivery(id, existingDelivery);
            var editedDeliveryDto = mapper.Map<DeliveryDTO>(editedDelivery);


            return Ok(editedDeliveryDto);
        }

        #endregion

        #region Edit Delivery Status

        [HttpPut("ChangeStatus/{id}")]
        [Authorize(Permissions.Deliveries.Edit)]
        [swagger.SwaggerOperation(Summary = "Update Delivery Status.")]
        [swagger.SwaggerResponse(StatusCodes.Status201Created, "Status successfully Retrieved.")]
        [swagger.SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data. Please check the provided information.")]

        public async Task<IActionResult> ChangeDeliveryStatus(string id, bool status)
        {
            var delivery = await unitOfWork.DeliveryRepository.GetById(id);
            if (delivery == null)
            {
                return NotFound("Delivery not found.");
            }

            unitOfWork.DeliveryRepository.UpdateStatus(delivery, status);
            return NoContent();
        }

        #endregion

        #region Delete Delivery

        [HttpDelete("DeleteDelivery/{id}")]
        [Authorize(Permissions.Deliveries.Delete)]
        [swagger.SwaggerOperation(Summary = "Delete Delivery.")]
        [swagger.SwaggerResponse(StatusCodes.Status201Created, "Delivery successfully Deleted.")]
        [swagger.SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data. Please check the provided information.")]
        public async Task<IActionResult> SoftDeleteDelivery(string id)
        {
            try
            {
                var delivery = await unitOfWork.DeliveryRepository.GetById(id);
                if (delivery != null)
                {
                    await unitOfWork.DeliveryRepository.SoftDeleteAsync(delivery);
                    unitOfWork.SaveChanges();
                    return Ok("Delivery deleted successfully.");
                }
                return NotFound("Delivery not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
