using AirCoil_API.Dto;
using AirCoil_API.Helpers;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirCoil_API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IModelRepository _modelRepository;

        public CarController(ICarRepository carRepository, IMapper mapper, IProvinceRepository provinceRepository, IModelRepository modelRepository)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _provinceRepository = provinceRepository;
            _modelRepository = modelRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCars([FromQuery] CarQueryObject query)
        {
            var cars = _mapper.Map<List<CarDto>>(await _carRepository.GetCarsAsync(query));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(cars);
        }

        [HttpGet("{carId}")]
        [ProducesResponseType(200, Type = typeof(CarDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCar(int carId)
        {
            if (!await _carRepository.CarExistsAsync(carId))
            {
                return NotFound();
            }

            var car = _mapper.Map<CarDto>(await _carRepository.GetCarAsync(carId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(car);
        }

        [HttpGet("{carId}/jobs")]
        [ProducesResponseType(200, Type = typeof(PagedResult<JobDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetJobsByCar(int carId, [FromQuery] JobQueryObject query)
        {
            if (!await _carRepository.CarExistsAsync(carId))
            {
                return NotFound();
            }

            var jobs = _mapper.Map<List<JobDto>>(await _carRepository.GetJobsByCarAsync(carId, query));

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

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarDto carCreate)
        {
            if (carCreate == null)
            {
                return BadRequest(ModelState);
            }

            var carMap = _mapper.Map<Car>(carCreate);
            carMap.Province = await _provinceRepository.GetProvinceAsync(carCreate.Province);
            carMap.Model = await _modelRepository.GetModelAsync(carCreate.Model);

            if (carMap.Model == null || carMap.Province == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _carRepository.CarExistsAsync(carMap))
            {
                ModelState.AddModelError("", "Car already exists");
                return StatusCode(422, ModelState);
            }

            if (!await _carRepository.CreateCarAsync(carMap))
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            return Created();
        }

        [HttpPatch("{carId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCar(int carId, [FromQuery] int provinceId, [FromQuery] int modelId, [FromBody] CreateCarDto updatedCar) 
        {
            if (!await _carRepository.CarExistsAsync(carId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carMap = _mapper.Map<Car>(updatedCar);
            carMap.Id = carId;
            carMap.Province = await _provinceRepository.GetProvinceAsync(provinceId);
            carMap.Model = await _modelRepository.GetModelAsync(modelId);

            if (!await _carRepository.UpdateCarAsync(carMap))
            {
                ModelState.AddModelError("", "Error occur while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{carId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(405)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteModel(int carId)
        {
            if (!await _carRepository.CarExistsAsync(carId))
            {
                return NotFound();
            }

            var carToDelete = await _carRepository.GetCarAsync(carId);

            if ((await _carRepository.GetJobsByCarAsync(carId, null)).Count() > 0)
            {
                ModelState.AddModelError("", $"There's a job entities that has a relation with car id: {carId}");
                return StatusCode(405, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _carRepository.DeleteCarAsync(carToDelete))
            {
                ModelState.AddModelError("", "Error occur while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
