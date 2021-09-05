using _4TeamTask.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4TeamTask.Services
{
    public interface IUserService
    {
        Task<IEnumerable<WeatherForecast>> GetUserData(string userId);

        Task<IEnumerable<WeatherForecast>> SaveUserData(string userId);
    }
}
