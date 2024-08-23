using AirCoil_API.Dto;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirCoil_API.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class ModelController : Controller
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public ModelController(IModelRepository modelRepository, IMapper mapper, IBrandRepository brandRepository)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ModelDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetModels()
        {
            var models = _mapper.Map<List<ModelDto>>(_modelRepository.GetModels());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(models);
        }

        [HttpGet("{modelId}")]
        [ProducesResponseType(200, Type = typeof(ModelDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetModel(int modelId)
        {
            if (!_modelRepository.ModelExists(modelId))
            {
                return NotFound();
            }

            var model = _mapper.Map<ModelDto>(_modelRepository.GetModel(modelId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateModel([FromQuery] int brandId, [FromBody] CreateModelDto modelCreate)
        {
            if (modelCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (_modelRepository.ModelExists(modelCreate.Name))
            {
                ModelState.AddModelError("", "Model already exists");
                return StatusCode(422, ModelState);
            }

            if (!_brandRepository.BrandExists(brandId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelMap = _mapper.Map<Model>(modelCreate);
            modelMap.Brand = _brandRepository.GetBrand(brandId);

            if (!_modelRepository.CreateModel(modelMap))
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            return Created();
        }

        [HttpPatch("{modelId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateModel(int modelId, [FromQuery] int brandId, [FromBody] CreateModelDto updatedModel)
        {
            if (updatedModel == null)
            {
                return BadRequest(ModelState);
            }

            if (!_modelRepository.ModelExists(modelId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelMap = _mapper.Map<Model>(updatedModel);
            modelMap.Id = modelId;
            modelMap.Brand = _brandRepository.GetBrand(brandId);

            if (!_modelRepository.UpdateModel(modelMap))
            {
                ModelState.AddModelError("", "Error occur while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{modelId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(405)]
        [ProducesResponseType(500)]
        public IActionResult DeleteModel(int modelId)
        {
            if (!_modelRepository.ModelExists(modelId))
            {
                return NotFound();
            }

            var modelToDelete = _modelRepository.GetModel(modelId);

            if (_modelRepository.GetCarsByModel(modelId).Count() > 0)
            {
                ModelState.AddModelError("", $"There's a car entities that has a relation with model id: {modelId}");
                return StatusCode(405, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_modelRepository.DeleteModel(modelToDelete))
            {
                ModelState.AddModelError("", "Error occur while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
