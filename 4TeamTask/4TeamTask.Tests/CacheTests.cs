using _4TeamTask.Common.Cache;
using _4TeamTask.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _4TeamTask.Tests
{
    [TestClass]
    public class CacheTests
    {
        ICacheProvider _cacheProvider;
        private string cacheKey = "People";

        [TestInitialize]
        public void Initialize()
        {
            _cacheProvider = new RedisCacheProvider();
        }

        [TestMethod]
        public void CacheTests_SetValue()
        {
            _cacheProvider.Set(cacheKey, GetPeopleData());
        }

        [TestMethod]
        public void CacheTests_SetValueTimeout()
        {
            EnsureDataIsNotCached(cacheKey);

            _cacheProvider.Set(cacheKey, GetPeopleData(), TimeSpan.FromSeconds(1));

            System.Threading.Thread.Sleep(2000);

            var people = _cacheProvider.Get<List<Person>>(cacheKey);

            Assert.IsNull(people);
        }

        [TestMethod]
        public void CacheTests_GetValue()
        {
            EnsureDataIsNotCached(cacheKey);

            _cacheProvider.Set(cacheKey, GetPeopleData());

            var people = _cacheProvider.Get<List<Person>>(cacheKey);

            Assert.IsNotNull(people);
            Assert.AreEqual("Joe", people.FirstOrDefault().Name);
        }

        [TestMethod]
        public void CacheTests_IsInCache()
        {
            EnsureDataIsNotCached(cacheKey);

            _cacheProvider.Set(cacheKey, GetPeopleData());

            var isInCache = _cacheProvider.IsInCache(cacheKey);

            Assert.IsTrue(isInCache);
        }

        [TestMethod]
        public void CacheTests_RemoveFromCache()
        {
            EnsureDataIsNotCached(cacheKey);

            _cacheProvider.Set("People", GetPeopleData());

            var isRemoved = _cacheProvider.Remove(cacheKey);

            var isInCache = _cacheProvider.IsInCache(cacheKey);

            var people = _cacheProvider.Get<List<Person>>(cacheKey);

            Assert.IsTrue(isRemoved);
            Assert.IsFalse(isInCache);
            Assert.IsNull(people);
        }

        private List<Person> GetPeopleData()
        {
            List<Person> people = new List<Person>()
            {
                new Person(1, "Joe", new List<Contact>()
                {
                    new Contact("1", "123456789"),
                    new Contact("2", "234567890")
                })
            };

            return people;
        }

        private void EnsureDataIsNotCached(string key)
        {
            _cacheProvider.Remove(key);
        }
    }
}
