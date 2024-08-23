using AirCoil_API.Dto;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirCoil_API.Controllers
{
    [Route("api/service-centers")]
    [ApiController]
    public class ServiceCenterController : Controller
    {
        private readonly IServiceCenterRepository _serviceCenterRepository;
        private readonly IMapper _mapper;

        public ServiceCenterController(IServiceCenterRepository serviceCenterRepository, IMapper mapper)
        {
            _serviceCenterRepository = serviceCenterRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ServiceCenterDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetServiceCenter()
        {
            var serviceCenters = _mapper.Map<List<ServiceCenter>>(_serviceCenterRepository.GetServiceCenters());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(serviceCenters);
        }

        [HttpGet("{serviceCenterId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ServiceCenterDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetServiceCenter(int serviceCenterId)
        {
            if (!_serviceCenterRepository.ServiceCenterExists(serviceCenterId))
            {
                return NotFound();
            }

            var serviceCenter = _mapper.Map<ServiceCenter>(_serviceCenterRepository.GetServiceCenter(serviceCenterId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(serviceCenter);
        }

        [HttpGet("{serviceCenterId}/branches")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ServiceCenterDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetBranchesByServiceCenter(int serviceCenterId)
        {
            if (!_serviceCenterRepository.ServiceCenterExists(serviceCenterId))
            {
                return NotFound();
            }

            var branches = _mapper.Map<List<Branch>>(_serviceCenterRepository.GetBranchesByServiceCenter(serviceCenterId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(branches);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateServiceCenter([FromBody] CreateServiceCenterDto serviceCenterCreate)
        {
            if (serviceCenterCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceCenterMap = _mapper.Map<ServiceCenter>(serviceCenterCreate);

            if (!_serviceCenterRepository.CreateServiceCenter(serviceCenterMap))
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            return Created();
        }

        [HttpPatch("{serviceCenterId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateProvince(int serviceCenterId, [FromBody] CreateServiceCenterDto updatedServiceCenter)
        {
            if (updatedServiceCenter == null)
            {
                return BadRequest(ModelState);
            }

            if (!_serviceCenterRepository.ServiceCenterExists(serviceCenterId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceCenterMap = _mapper.Map<ServiceCenter>(updatedServiceCenter);
            serviceCenterMap.Id = serviceCenterId;

            if (!_serviceCenterRepository.UpdateServiceCenter(serviceCenterMap))
            {
                ModelState.AddModelError("", "Error occur while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{serviceCenterId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(405)]
        [ProducesResponseType(500)]
        public IActionResult DeleteProvince(int serviceCenterId)
        {
            if (!_serviceCenterRepository.ServiceCenterExists(serviceCenterId))
            {
                return NotFound();
            }
            var provinceToDelete = _serviceCenterRepository.GetServiceCenter(serviceCenterId);

            if (_serviceCenterRepository.GetBranchesByServiceCenter(serviceCenterId).Count() > 0)
            {
                ModelState.AddModelError("", $"There's a branch entities that has a relation with serviceCenter id: {serviceCenterId}");
                return StatusCode(405, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_serviceCenterRepository.DeleteServiceCenter(provinceToDelete))
            {
                ModelState.AddModelError("", "Error occur while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
