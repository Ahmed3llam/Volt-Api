using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Models;
using Shipping.Repository.GovernmentRepo;

namespace Shipping.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GovernmentController : ControllerBase
    {
        private readonly IGovernmentRepository _governmentRepository;

        public GovernmentController(IGovernmentRepository governmentRepository)
        {
            _governmentRepository = governmentRepository;
        }

        [HttpGet]
 //      [Authorize(Permissions.Governments.View)]
        public ActionResult<IEnumerable<Government>> GetAllGovernments()
        {
            var governments = _governmentRepository.GetAll();
            return Ok(governments);
        }

        [HttpPost("add")]
    //    [Authorize(Permissions.Governments.Create)]
        public async Task<ActionResult> AddGovernment([FromBody] Government government)
        {
            if (ModelState.IsValid)
            {
                _governmentRepository.Add(government);
                return CreatedAtAction(nameof(GetGovernmentById), new { id = government.Id }, government);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("edit/{id}")]
     //   [Authorize(Permissions.Governments.Edit)]
        public ActionResult<Government> GetGovernmentForEdit(int id)
        {
            var government = _governmentRepository.GetById(id);
            if (government == null)
            {
                return NotFound();
            }
            return Ok(government);
        }

        [HttpPut("edit/{id}")]
 //       [Authorize(Permissions.Governments.Edit)]
        public IActionResult EditGovernment(int id, [FromBody] Government government)
        {
            if (id != government.Id)
            {
                return BadRequest();
            }

            _governmentRepository.Update(id, government);
            return NoContent();
        }

        [HttpPut("change-status/{id}")]
  //      [Authorize(Permissions.Governments.Edit)]
        public IActionResult ChangeGovernmentStatus(int id, [FromQuery] bool status)
        {
            var government = _governmentRepository.GetById(id);
            if (government == null)
            {
                return NotFound();
            }

            _governmentRepository.UpdateStatus(government, status);
            return NoContent();
        }

        [HttpGet("search")]
   //     [Authorize(Permissions.Governments.View)]
        public ActionResult<IEnumerable<Government>> SearchGovernments([FromQuery] string query)
        {
            List<Government> governments;
            if (string.IsNullOrWhiteSpace(query))
            {
                governments = _governmentRepository.GetAll().ToList();
            }
            else
            {
                governments = _governmentRepository.GetAll().Where(g => g.Name.Contains(query)).ToList();
            }

            return Ok(governments);
        }

        [HttpDelete("delete/{id}")]
    //    [Authorize(Permissions.Governments.Delete)]
        public IActionResult DeleteGovernment(int id)
        {
            var government = _governmentRepository.GetById(id);
            if (government == null)
            {
                return NotFound();
            }

            government.IsDeleted = true;
            _governmentRepository.Update(id, government);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Government> GetGovernmentById(int id)
        {
            var government = _governmentRepository.GetById(id);
            if (government == null)
            {
                return NotFound();
            }
            return Ok(government);
        }
    }
}
