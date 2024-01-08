using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Models;
using Project.Repositories;


namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _repository;
        private readonly IMapper _mapper;

        public AppUserController(IAppUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppUserDto>))]
        public IActionResult GetAllUsers()
        {
            var users = _repository.GetAllAppUsers();
            var userDtos = _mapper.Map<IEnumerable<AppUserDto>>(users);

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AppUserDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUserById(int id)
        {
            var user = _repository.GetAppUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<AppUserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAppUser([FromBody] AppUserDto createAppUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = _mapper.Map<AppUser>(createAppUserDto);
            appUser.UserId = 0;
            _repository.AddAppUser(appUser);

            return CreatedAtAction(nameof(GetUserById), new { id = appUser.UserId }, appUser);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAppUser(int id, [FromBody] AppUserDto updateAppUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = _repository.GetAppUserById(id);
            if (appUser == null)
            {
                return NotFound();
            }

            _mapper.Map(updateAppUserDto, appUser);
            appUser.UserId = id;
            _repository.UpdateAppUser(appUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAppUser(int id)
        {
            var appUser = _repository.GetAppUserById(id);
            if (appUser == null)
            {
                return NotFound();
            }

            _repository.DeleteAppUser(id);
            return NoContent();
        }

        [HttpGet("MealsByUserId/{userId}")]
        public ActionResult<IEnumerable<Meal>> GetMealsByUserId(int userId)
        {
            var meals = _repository.GetMealsByUserId(userId);
            if (meals == null || meals.Count == 0)
            {
                return NotFound($"Żadne posiłki nie zostały znalezione dla użytkownika o ID {userId}.");
            }

            return Ok(meals);
        }
    }
}
