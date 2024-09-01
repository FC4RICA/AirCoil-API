using AirCoil_API.Dto;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirCoil_API.Controllers
{
    [Route("api/provinces")]
    [ApiController]
    public class ProvinceController : Controller
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IMapper _mapper;

        public ProvinceController(IProvinceRepository provinceRepository, IMapper mapper)
        {
            _provinceRepository = provinceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProvinceDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProvinces()
        {
            var provinces = _mapper.Map<List<ProvinceDto>>(await _provinceRepository.GetProvicesAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(provinces);
        }
        
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateProvince([FromBody] CreateProvinceDto provinceCreate)
        {
            if (provinceCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (await _provinceRepository.ProvinceExistsAsync(provinceCreate.Name))
            {
                ModelState.AddModelError("", "Province already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var provinceMap = _mapper.Map<Province>(provinceCreate);

            if (!(await _provinceRepository.CreateProvinceAsync(provinceMap)))
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            return Created();
        }

        [HttpPatch("{provinceId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateProvince(int provinceId, [FromBody]CreateProvinceDto updatedProvince)
        {
            if (updatedProvince == null)
            {
                return BadRequest(ModelState);
            }

            if (!await _provinceRepository.ProvinceExistsAsync(provinceId)) 
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var provinceMap = _mapper.Map<Province>(updatedProvince);
            provinceMap.Id = provinceId;

            if (!await _provinceRepository.UpdateProvinceAsync(provinceMap))
            {
                ModelState.AddModelError("", "Error occur while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{provinceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(405)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteProvince(int provinceId)
        {
            if (!await _provinceRepository.ProvinceExistsAsync(provinceId))
            {
                return NotFound();
            }

            var provinceToDelete = await _provinceRepository.GetProvinceAsync(provinceId);

            if ((await _provinceRepository.GetCarsByProvinceAsync(provinceId)).Count() > 0)
            {
                ModelState.AddModelError("", $"There's a car entities that has a relation with province id: {provinceId}");
                return StatusCode(405, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _provinceRepository.DeleteProvinceAsync(provinceToDelete))
            {
                ModelState.AddModelError("", "Error occur while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
