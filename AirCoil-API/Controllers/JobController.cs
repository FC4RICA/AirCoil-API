﻿using AirCoil_API.Dto;
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
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateJob(IFormFileCollection files, [FromForm] CreateJobDto jobCreate)
        {
            if (files.IsNullOrEmpty())
            {
                return BadRequest(ModelState);
            }

            var car = await _carRepository.GetCarAsync(jobCreate.Car);

            if (car == null)
            {
                return NotFound();
            }

            var images = await _imageService.CreateImageAsync(files);

            if (images.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            var result = await _resultRepository.GetResultAsync(1);

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var job = new Job
            {
                Mileage = jobCreate.Mileage,
                Car = car,
                Images = images,
                Result = result,
                User = user
            };

            if (!await _jobRepository.CreateJobAsync(job))
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            Task.Run(() => _predictionService.HandlePredictionAsync(job));

            return CreatedAtAction(nameof(GetJob), job);
        }
    }
}
