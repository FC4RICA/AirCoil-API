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
        public async Task<IActionResult> GetModels()
        {
            var models = _mapper.Map<List<ModelDto>>(await _modelRepository.GetModelsAsync());

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
        public async Task<IActionResult> GetModel(int modelId)
        {
            if (!await _modelRepository.ModelExistsAsync(modelId))
            {
                return NotFound();
            }

            var model = _mapper.Map<ModelDto>(await _modelRepository.GetModelAsync(modelId));

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
        public async Task<IActionResult> CreateModel([FromQuery] int brandId, [FromBody] CreateModelDto modelCreate)
        {
            if (modelCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (await _modelRepository.ModelExistsAsync(modelCreate.Name))
            {
                ModelState.AddModelError("", "Model already exists");
                return StatusCode(422, ModelState);
            }

            if (!await _brandRepository.BrandExistsAsync(brandId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelMap = _mapper.Map<Model>(modelCreate);
            modelMap.Brand = await _brandRepository.GetBrandAsync(brandId);

            if (!await _modelRepository.CreateModelAsync(modelMap))
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
        public async Task<IActionResult> UpdateModel(int modelId, [FromQuery] int brandId, [FromBody] CreateModelDto updatedModel)
        {
            if (updatedModel == null)
            {
                return BadRequest(ModelState);
            }

            if (!await _modelRepository.ModelExistsAsync(modelId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelMap = _mapper.Map<Model>(updatedModel);
            modelMap.Id = modelId;
            modelMap.Brand = await _brandRepository.GetBrandAsync(brandId);

            if (!await _modelRepository.UpdateModelAsync(modelMap))
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
        public async Task<IActionResult> DeleteModel(int modelId)
        {
            if (!await _modelRepository.ModelExistsAsync(modelId))
            {
                return NotFound();
            }

            var modelToDelete = await _modelRepository.GetModelAsync(modelId);

            if ((await _modelRepository.GetCarsByModelAsync(modelId)).Count() > 0)
            {
                ModelState.AddModelError("", $"There's a car entities that has a relation with model id: {modelId}");
                return StatusCode(405, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _modelRepository.DeleteModelAsync(modelToDelete))
            {
                ModelState.AddModelError("", "Error occur while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
