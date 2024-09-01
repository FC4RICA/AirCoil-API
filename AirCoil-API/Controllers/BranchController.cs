using AirCoil_API.Dto;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirCoil_API.Controllers
{
    [Route("api/branches")]
    [ApiController]
    public class BranchController : Controller
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly IServiceCenterRepository _serviceCenterRepository;

        public BranchController(IBranchRepository branchRepository, IMapper mapper, IServiceCenterRepository serviceCenterRepository)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
            _serviceCenterRepository = serviceCenterRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BranchDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBranches()
        {
            var branches = _mapper.Map<ICollection<BrandDto>>(await _branchRepository.GetBranchesAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(branches);
        }

        [HttpGet("{branchId}")]
        [ProducesResponseType(200, Type = typeof(BranchDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBranch(int branchId)
        {
            if (!await _branchRepository.BranchExistsAsync(branchId))
            {
                return NotFound();
            }

            var branch = _mapper.Map<BranchDto>(await _branchRepository.GetBranchAsync(branchId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(branch);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateBranch([FromQuery] int serviceCenterId, [FromBody] CreateBranchDto branchCreate)
        {
            if (branchCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!await _serviceCenterRepository.ServiceCenterExistsAsync(serviceCenterId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branchMap = _mapper.Map<Branch>(branchCreate);
            branchMap.ServiceCenter = await _serviceCenterRepository.GetServiceCenterAsync(serviceCenterId);

            if (!await _branchRepository.CreateBranchAsync(branchMap))
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            return Created();
        }

        [HttpPatch("{branchId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateModel(int branchId, [FromQuery] int serviceCenterId ,[FromBody] CreateBranchDto updatedBranch)
        {
            if (updatedBranch == null)
            {
                return BadRequest(ModelState);
            }

            if (!await _branchRepository.BranchExistsAsync(branchId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branchMap = _mapper.Map<Branch>(updatedBranch);
            branchMap.Id = branchId;
            branchMap.ServiceCenter = await _serviceCenterRepository.GetServiceCenterAsync(serviceCenterId);

            if (!await _branchRepository.UpdateBranchAsync(branchMap))
            {
                ModelState.AddModelError("", "Error occur while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{branchId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(405)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteModel(int branchId)
        {
            if (!await _branchRepository.BranchExistsAsync(branchId))
            {
                return NotFound();
            }

            var branchToDelete = await _branchRepository.GetBranchAsync(branchId);

            if ((await _branchRepository.GetUserByBranchAsync(branchId)).Count() > 0)
            {
                ModelState.AddModelError("", $"There's a user entities that has a relation with branch id: {branchId}");
                return StatusCode(405, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _branchRepository.DeleteBranchAsync(branchToDelete))
            {
                ModelState.AddModelError("", "Error occur while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
