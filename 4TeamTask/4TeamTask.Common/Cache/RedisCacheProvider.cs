﻿using ServiceStack.Redis;
using System;

namespace _4TeamTask.Common.Cache
{
    public class RedisCacheProvider : ICacheProvider
    {
        RedisEndpoint _endPoint;
        public RedisCacheProvider(string host = Constants.RedisHostName, int port = Constants.RedisPort)
        {
            _endPoint = new RedisEndpoint(host, port);
        }

        public void Set<T>(string key, T value)
        {
            this.Set(key, value, TimeSpan.Zero);
        }

        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.As<T>().SetValue(key, value, timeout);
            }
        }

        public T Get<T>(string key)
        {
            T result = default(T);

            using (RedisClient client = new RedisClient(_endPoint))
            {
                var wrapper = client.As<T>();

                result = wrapper.GetValue(key);
            }

            return result;
        }

        public bool Remove(string key)
        {
            bool removed = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                removed = client.Remove(key);
            }

            return removed;
        }

        public bool IsInCache(string key)
        {
            bool isInCache = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                isInCache = client.ContainsKey(key);
            }

            return isInCache;
        }

        public T GetOrSet<T>(string userId, T data)
        {
            if (IsInCache(userId))
            {
                return Get<T>(userId);
            }
            else
            {
                Set(userId, data);
                return data;
            }
        }
    }
}