using Project.Data;
using Project.Interfaces;
using Project.Models;


namespace Project.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly DataContext _context;

        public AppUserRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<AppUser> GetAllAppUsers()
        {
            return _context.AppUsers.OrderBy(p => p.UserId).ToList();
        }

        public AppUser GetAppUserById(int id)
        {
            var user = _context.AppUsers.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"Użytkownik o ID {id} nie został znaleziony.");
            }

            return user;
        }

        public void AddAppUser(AppUser appUser)
        {
            _context.AppUsers.Add(appUser);
            _context.SaveChanges();
        }

        public void UpdateAppUser(AppUser appUser)
        {
            _context.AppUsers.Update(appUser);
            _context.SaveChanges();
        }

        public void DeleteAppUser(int id)
        {
            var appUser = _context.AppUsers.Find(id);
            if (appUser != null)
            {
                _context.AppUsers.Remove(appUser);
                _context.SaveChanges();
            }
        }
        public ICollection<Meal> GetMealsByUserId(int userId)
        {
            var meals = _context.UserMeals
                .Where(um => um.UserId == userId)
                .Select(um => um.Meal)
                .ToList();

            return meals;
        }
    }
}
