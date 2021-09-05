using System;

namespace _4TeamTask.Common.Cache
{
    public interface ICacheProvider
    {
        void Set<T>(string key, T value);

        void Set<T>(string key, T value, TimeSpan timeout);

        T Get<T>(string key);

        bool Remove(string key);

        bool IsInCache(string key);

        T GetOrSet<T>(string userId, T data);
    }
}
