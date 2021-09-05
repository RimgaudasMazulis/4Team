using _4TeamTask.Common;
using _4TeamTask.Common.Cache;
using _4TeamTask.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4TeamTask.Services
{
    public class UserService : IUserService
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IWeatherForecastsRepository _repository;
        public UserService(ICacheProvider cacheProvider, IWeatherForecastsRepository repository)
        {
            _cacheProvider = cacheProvider;
            _repository = repository;
        }

        public async Task<IEnumerable<WeatherForecast>> GetUserData(string userId)
        {
            if (_cacheProvider.IsInCache(userId))
            {
                return _cacheProvider.Get<IEnumerable<WeatherForecast>>(userId);
            }
            else
            {
                return await SaveUserDataInCache(userId);
            }
        }

        public async Task<IEnumerable<WeatherForecast>> SaveUserData(string userId)
        {
            return await SaveUserDataInCache(userId);
        }

        private async Task<IEnumerable<WeatherForecast>> SaveUserDataInCache(string userId)
        {
            var userData = await _repository.GetWeatherForecasts();
            _cacheProvider.Set(userId, userData, TimeSpan.FromMinutes(Constants.RedisCacheTimeoutInMinutes));
            return userData;
        }
    }
}
