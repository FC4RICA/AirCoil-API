using AirCoil_API.Dto;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirCoil_API.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandController(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BrandDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetBrands()
        {
            var brands = _mapper.Map<List<BrandDto>>(_brandRepository.GetBrands());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(brands);
        }

        [HttpGet("{brandId}")]
        [ProducesResponseType(200, Type = typeof(BrandDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetBrand(int brandId)
        {
            if (!_brandRepository.BrandExists(brandId))
            {
                return NotFound();
            }

            var brand = _mapper.Map<BrandDto>(_brandRepository.GetBrand(brandId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(brand);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateBrand([FromBody] CreateBrandDto brandCreate)
        {
            if (brandCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (_brandRepository.BrandExists(brandCreate.Name))
            {
                ModelState.AddModelError("", "Brand already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brandMap = _mapper.Map<Brand>(brandCreate);

            if (!_brandRepository.CreateBrand(brandMap))
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            return Created();
        }

        [HttpPatch("{brandId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateBrand(int brandId,[FromBody] CreateBrandDto updatedBrand)
        {
            if (updatedBrand == null)
            {
                return BadRequest(ModelState);
            }

            if (!_brandRepository.BrandExists(brandId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brandMap = _mapper.Map<Brand>(updatedBrand);
            brandMap.Id = brandId;

            if (!_brandRepository.UpdateBrand(brandMap))
            {
                ModelState.AddModelError("", "Error occur while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{brandId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(405)]
        [ProducesResponseType(500)]
        public IActionResult DeleteBrand(int brandId)
        {
            if (!_brandRepository.BrandExists(brandId))
            {
                return NotFound();
            }

            var brandToDelete = _brandRepository.GetBrand(brandId);

            if (_brandRepository.GetModelsByBrand(brandId).Count() > 0)
            {
                ModelState.AddModelError("", $"There's a model entities that has a relation with brand id: {brandId}");
                return StatusCode(405, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_brandRepository.DeleteBrand(brandToDelete))
            {
                ModelState.AddModelError("", "Error occur while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("{brandId}/models")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BrandDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetModelsByBrand(int brandId)
        {
            if (!_brandRepository.BrandExists(brandId))
            {
                return NotFound();
            }

            var models = _mapper.Map<List<ModelDto>>(_brandRepository.GetModelsByBrand(brandId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(models);
        }
    }
}
