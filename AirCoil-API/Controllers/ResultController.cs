using AirCoil_API.Dto;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using AirCoil_API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirCoil_API.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class ResultController : Controller
    {
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;

        public ResultController(IResultRepository resultRepository, IMapper mapper)
        {
            _resultRepository = resultRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ResultDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetResults()
        {
            var results = _mapper.Map<List<ResultDto>>(await _resultRepository.GetResultsAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(results);
        }

        [HttpGet("{resultId}")]
        [ProducesResponseType(200, Type = typeof(ResultDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetResult(int resultId)
        {
            if (!await _resultRepository.ResultExistsAsync(resultId))
            {
                return NotFound();
            }

            var result = _mapper.Map<ResultDto>(await _resultRepository.GetResultAsync(resultId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateResult([FromBody] CreateResultDto resultCreate)
        {
            if (resultCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultMap = _mapper.Map<Result>(resultCreate);

            if (!await _resultRepository.CreateResultAsync(resultMap))
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            return Created();
        }

        [HttpPatch("{resultId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBrand(int resultId, [FromBody] CreateResultDto updatedResult)
        {
            if (updatedResult == null)
            {
                return BadRequest(ModelState);
            }

            if (!await _resultRepository.ResultExistsAsync(resultId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultMap = _mapper.Map<Result>(updatedResult);
            resultMap.Id = resultId;

            if (!await _resultRepository.UpdateResultAsync(resultMap))
            {
                ModelState.AddModelError("", "Error occur while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{resultId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(405)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteResult(int resultId)
        {
            if (!await _resultRepository.ResultExistsAsync(resultId))
            {
                return NotFound();
            }

            var resultToDelete = await _resultRepository.GetResultAsync(resultId);

            if ((await _resultRepository.GetJobsByResultAsync(resultId)).Count() > 0)
            {
                ModelState.AddModelError("", $"There's a job entities that has a relation with result id: {resultId}");
                return StatusCode(405, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _resultRepository.DeleteResultAsync(resultToDelete))
            {
                ModelState.AddModelError("", "Error occur while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
