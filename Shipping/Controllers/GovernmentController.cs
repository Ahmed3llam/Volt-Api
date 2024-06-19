using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Models;
using Shipping.Repository.GovernmentRepo;
using Shipping.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace Shipping.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GovernmentController : ControllerBase
    {
        private readonly IUnitOfWork<Government> _unit;

        public GovernmentController(IUnitOfWork<Government> unit)
        {
            _unit = unit;
        }

        #region GetAllGovernments
        [HttpGet]
        // [Authorize(Permissions.Governments.View)]
        [SwaggerOperation(Summary = "Gets all governments.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of governments.")]
        public ActionResult<IEnumerable<Government>> GetAllGovernments()
        {
            var governments = _unit.GovernmentRepository.GetAll();
            return Ok(governments);
        }
        #endregion

        #region AddGovernment
        [HttpPost("add")]
        // [Authorize(Permissions.Governments.Create)]
        [SwaggerOperation(Summary = "Adds a new government.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Government successfully created.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data. Please check the provided information.")]
        public async Task<ActionResult> AddGovernment([FromBody] Government government)
        {
            if (ModelState.IsValid)
            {
                _unit.GovernmentRepository.Add(government);
                return CreatedAtAction(nameof(GetGovernmentById), new { id = government.Id }, government);
            }

            return BadRequest("Invalid data. Please check the provided information.");
        }
        #endregion

        #region EditGovernment
        [HttpPut("edit/{id}")]
        // [Authorize(Permissions.Governments.Edit)]
        [SwaggerOperation(Summary = "Edits an existing government.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Government successfully updated.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The ID in the URL does not match the ID in the body.")]
        public IActionResult EditGovernment(int id, [FromBody] Government government)
        {
            if (id != government.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the body.");
            }

            _unit.GovernmentRepository.Update(id, government);
            return NoContent();
        }
        #endregion

        #region ChangeGovernmentStatus
        [HttpPut("change-status/{id}")]
        // [Authorize(Permissions.Governments.Edit)]
        [SwaggerOperation(Summary = "Changes the status of a government.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Government status successfully updated.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Government not found.")]
        public IActionResult ChangeGovernmentStatus(int id, [FromQuery] bool status)
        {
            var government = _unit.GovernmentRepository.GetById(id);
            if (government == null)
            {
                return NotFound("Government not found.");
            }

            _unit.GovernmentRepository.UpdateStatus(government, status);
            return NoContent();
        }
        #endregion

        #region SearchGovernments
        [HttpGet("search")]
        // [Authorize(Permissions.Governments.View)]
        [SwaggerOperation(Summary = "Searches for governments by name.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of governments matching the search criteria.")]
        public ActionResult<IEnumerable<Government>> SearchGovernments([FromQuery] string query)
        {
            List<Government> governments;
            if (string.IsNullOrWhiteSpace(query))
            {
                governments = _unit.GovernmentRepository.GetAll().ToList();
            }
            else
            {
                governments = _unit.GovernmentRepository.GetAll().Where(g => g.Name.Contains(query)).ToList();
            }

            return Ok(governments);
        }
        #endregion

        #region DeleteGovernment
        [HttpDelete("delete/{id}")]
        // [Authorize(Permissions.Governments.Delete)]
        [SwaggerOperation(Summary = "Deletes a government by marking it as deleted.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Government successfully marked as deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Government not found.")]
        public IActionResult DeleteGovernment(int id)
        {
            var government = _unit.GovernmentRepository.GetById(id);
            if (government == null)
            {
                return NotFound("Government not found.");
            }

            government.IsDeleted = true;
            _unit.GovernmentRepository.Update(id, government);
            return NoContent();
        }
        #endregion

        #region GetGovernmentById
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a government by ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the government object.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Government not found.")]
        public ActionResult<Government> GetGovernmentById(int id)
        {
            var government = _unit.GovernmentRepository.GetById(id);
            if (government == null)
            {
                return NotFound("Government not found.");
            }
            return Ok(government);
        }
        #endregion
    }
}
