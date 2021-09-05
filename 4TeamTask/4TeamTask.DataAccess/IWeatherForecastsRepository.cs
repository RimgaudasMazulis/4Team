using _4TeamTask.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4TeamTask.DataAccess
{
    public interface IWeatherForecastsRepository
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();
    }
}