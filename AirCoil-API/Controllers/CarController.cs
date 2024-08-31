using AirCoil_API.Dto;
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
        public IActionResult GetCars()
        {
            var cars = _mapper.Map<List<CarDto>>(_carRepository.GetCars());

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
        public IActionResult GetCar(int carId)
        {
            if (!_carRepository.CarExists(carId))
            {
                return NotFound();
            }

            var car = _mapper.Map<CarDto>(_carRepository.GetCar(carId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(car);
        }

        [HttpGet("{carId}/jobs")]
        [ProducesResponseType(200, Type = typeof(ICollection<JobDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetJobsByCar(int carId)
        {
            if (!_carRepository.CarExists(carId))
            {
                return NotFound();
            }

            var jobs = _mapper.Map<List<JobDto>>(_carRepository.GetJobsByCar(carId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(jobs);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateCar([FromQuery] int provinceId, [FromQuery] int modelId, [FromBody] CreateCarDto carCreate)
        {
            if (carCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!_modelRepository.ModelExists(modelId) || !_provinceRepository.ProvinceExists(provinceId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carMap = _mapper.Map<Car>(carCreate);
            carMap.Province = _provinceRepository.GetProvince(provinceId);
            carMap.Model = _modelRepository.GetModel(modelId);

            if (_carRepository.CarExists(carMap))
            {
                ModelState.AddModelError("", "Car already exists");
                return StatusCode(422, ModelState);
            }

            if (!_carRepository.CreateCar(carMap))
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
        public IActionResult UpdateCar(int carId, [FromQuery] int provinceId, [FromQuery] int modelId, [FromBody] CreateCarDto updatedCar) 
        {
            if (!_carRepository.CarExists(carId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carMap = _mapper.Map<Car>(updatedCar);
            carMap.Id = carId;
            carMap.Province = _provinceRepository.GetProvince(provinceId);
            carMap.Model = _modelRepository.GetModel(modelId);

            if (!_carRepository.UpdateCar(carMap))
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
        public IActionResult DeleteModel(int carId)
        {
            if (!_carRepository.CarExists(carId))
            {
                return NotFound();
            }

            var carToDelete = _carRepository.GetCar(carId);

            if (_carRepository.GetJobsByCar(carId).Count() > 0)
            {
                ModelState.AddModelError("", $"There's a job entities that has a relation with car id: {carId}");
                return StatusCode(405, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_carRepository.DeleteCar(carToDelete))
            {
                ModelState.AddModelError("", "Error occur while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
