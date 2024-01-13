using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Models;


namespace Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserMealController : ControllerBase
    {
        private readonly IUserMealRepository _repository;
        private readonly IMapper _mapper;

        public UserMealController(IUserMealRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<UserMealDto>> GetUserMeals(int userId)
        {
            var userMeals = _repository.GetUserMeals(userId);
            var userMealDtos = _mapper.Map<IEnumerable<UserMealDto>>(userMeals);
            return Ok(userMealDtos);
        }

        [HttpPost]
        public IActionResult CreateUserMeal([FromBody] UserMealDto createUserMealDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMeal = _mapper.Map<UserMeal>(createUserMealDto);
            _repository.AddUserMeal(userMeal);

            return CreatedAtAction(nameof(GetUserMeals), new { userId = userMeal.UserId }, userMeal);
        }

        [HttpDelete("{userId}/{mealId}")]
        public IActionResult DeleteUserMeal(int userId, int mealId)
        {
            var userMeals = _repository.GetUserMeals(userId);
            var userMeal = userMeals.FirstOrDefault(um => um.UserId == userId && um.MealId == mealId);

            if (userMeal == null)
            {
                return NotFound();
            }

            _repository.DeleteUserMeal(userId, mealId);
            return NoContent();
        }
    }
}
