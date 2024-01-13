using Project.Models;


namespace Project.Interfaces
{
    public interface IAppUserRepository
    {
        ICollection<AppUser> GetAllAppUsers();
        AppUser GetAppUserById(int userId);
        void AddAppUser(AppUser user);
        void UpdateAppUser(AppUser user);
        void DeleteAppUser(int userId);
        ICollection<Meal> GetMealsByUserId(int userId);
        IEnumerable<Meal> GetMealsByUserAndDateRange(int userId, DateTime startDate, DateTime endDate);
    }
}