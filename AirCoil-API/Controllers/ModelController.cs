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

        public ModelController(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
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
    }
}
