using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Models;
using Shipping.Repository.CityRepo;
using Shipping.Repository.GovernmentRepo;

namespace Shipping.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IGovernmentRepository _governmentRepository;

        public CityController(ICityRepository cityRepository, IGovernmentRepository governmentRepository)
        {
            _cityRepository = cityRepository;
            _governmentRepository = governmentRepository;
        }

        [HttpGet("{governmentId}")]
        // [Authorize(Permissions.Cities.View)]
        public ActionResult<IEnumerable<City>> GetCitiesByGovernment(int governmentId)
        {
            var cities = _cityRepository.GetAllByGovernmentId(governmentId);
            return Ok(cities);
        }

        [HttpPost("add/{governmentId}")]
        // [Authorize(Permissions.Cities.Create)]
        public async Task<ActionResult> AddCity(int governmentId, [FromBody] City city)
        {
            if (ModelState.IsValid)
            {
                _cityRepository.AddToGovernment(governmentId, city);
                return CreatedAtAction(nameof(GetCityById), new { id = city.Id }, city);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("change-status/{id}")]
        // [Authorize(Permissions.Cities.Edit)]
        public ActionResult ChangeStatus(int id, [FromQuery] bool status)
        {
            var city = _cityRepository.GetById(id);
            if (city == null)
            {
                return NotFound();
            }
            city.Status = status;
            _cityRepository.Update(id, city);
            return NoContent();
        }

        [HttpGet("edit/{id}")]
        // [Authorize(Permissions.Cities.Edit)]
        public ActionResult<City> GetCityForEdit(int id)
        {
            var city = _cityRepository.GetById(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpPut("edit/{id}")]
        // [Authorize(Permissions.Cities.Edit)]
        public ActionResult EditCity(int id, [FromBody] City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }
            _cityRepository.Update(id, city);
            return NoContent();
        }

        [HttpGet("search")]
        // [Authorize(Permissions.Cities.View)]
        public ActionResult<IEnumerable<City>> SearchCities([FromQuery] int governmentId, [FromQuery] string query)
        {
            List<City> cities;
            if (string.IsNullOrWhiteSpace(query))
            {
                cities = _cityRepository.GetAllByGovernmentId(governmentId).ToList();
            }
            else
            {
                cities = _cityRepository.GetAllByGovernmentId(governmentId)
                    .Where(i => i.Name.ToUpper().Contains(query.ToUpper())).ToList();
            }
            return Ok(cities);
        }

        [HttpDelete("delete/{id}")]
        // [Authorize(Permissions.Cities.Delete)]
        public ActionResult DeleteCity(int id)
        {
            var city = _cityRepository.GetById(id);
            if (city == null)
            {
                return NotFound();
            }
            city.IsDeleted = true;
            _cityRepository.Update(id, city);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<City> GetCityById(int id)
        {
            var city = _cityRepository.GetById(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
