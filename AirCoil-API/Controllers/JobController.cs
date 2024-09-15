using AirCoil_API.Dto;
using AirCoil_API.Helpers;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using AirCoil_API.Service;
using AirCoil_API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AirCoil_API.Dto.Image;
using Microsoft.IdentityModel.Tokens;

namespace AirCoil_API.Controllers
{
    [Route("api/jobs")]
    [Controller]
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly ICarRepository _carRepository;
        private readonly IResultRepository _resultRepository;
        private readonly UserManager<User> _userManager;
        private readonly IPredictionService _predictionService;

        public JobController(IJobRepository jobRepository, IMapper mapper, IImageService imageService, ICarRepository carRepository, IResultRepository resultRepository, UserManager<User> userManager, IPredictionService predictionService)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _imageService = imageService;
            _carRepository = carRepository;
            _resultRepository = resultRepository;
            _userManager = userManager;
            _predictionService = predictionService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PagedResult<JobDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetJobs([FromQuery] JobQueryObject query)
        {
            var jobs = _mapper.Map<List<JobDto>>(await _jobRepository.GetJobsAsync(query));
            if (jobs.Any())
            {
                jobs.ForEach(j => j.Images.ToList().ForEach(async i => i.Url = await _imageService.GetImageUrlAsync(i.Id, Request)));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new PagedResult<JobDto>
            {
                Data = jobs,
                CurrentPage = query.PageNumber,
                PageSize = query.PageSize,
                TotalRecords = jobs.Count(),
                TotalPages = (int)Math.Ceiling(jobs.Count() / (double)query.PageSize)
            });
        }

        [Authorize]
        [HttpGet("{jobId}")]
        [ProducesResponseType(200, Type = typeof(JobDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetJob(int jobId)
        {
            var job = _mapper.Map<JobDto>(await _jobRepository.GetJobAsync(jobId));
            if (job == null)
            {
                return NotFound();
            }
            job.Images.ToList().ForEach(async i => i.Url = await _imageService.GetImageUrlAsync(i.Id, Request));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(job);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(JobDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateJob(IFormFile file, [FromForm] CreateJobDto jobCreate)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(ModelState);
            }

            var car = await _carRepository.GetCarAsync(jobCreate.Car);

            if (car == null)
            {
                return NotFound();
            }

            var image = await _imageService.GetImageAsync((await _imageService.CreateImageAsync(file)).Id);

            if (image == null)
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            var result = await _resultRepository.GetResultAsync(1);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId.IsNullOrEmpty())
                Console.WriteLine("USER ID IS EMPTY");

            var job = new Job
            {
                Mileage = jobCreate.Mileage,
                Car = car,
                Result = result,
                UserId = userId,
                Images = new List<Image>{ image }
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _jobRepository.CreateJobAsync(job))
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            await Task.Run(() => _predictionService.HandlePredictionAsync(job, Request) );

            var jobDto = _mapper.Map<JobDto>(job);

            return Ok(jobDto);
        }
    }
}
