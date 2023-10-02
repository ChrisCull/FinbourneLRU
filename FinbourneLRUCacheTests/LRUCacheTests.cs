using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinbourneLRUCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinbourneLRUCache.Tests
{
    [TestClass()]
    public class LRUCacheTests
    {
        [TestMethod()]
        public void GetFailure()
        {
            LRUCache<int, string> cache = new LRUCache<int, string>(5);
            var result = cache.get(2);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void GetSuccess()
        {
            LRUCache<int, string> cache = new LRUCache<int, string>(5);
            cache.put(2, "This is the value");
            var result = cache.get(2);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CapacityCheckSuccess()
        {
            LRUCache<int, string> cache = new LRUCache<int, string>(5);
            cache.put(1, "This is the value");
            cache.put(2, "This is another value");
            

            Assert.IsTrue(cache.CurrentCacheSize == 2);
        }

        [TestMethod()]
        public void CapacityCheckFail()
        {
            LRUCache<int, string> cache = new LRUCache<int, string>(5);
            cache.put(1, "This is the value");
            cache.put(2, "This is another value");


            Assert.IsFalse(cache.CurrentCacheSize == 4);
        }

        [TestMethod()]
        public void TestPut()
        {
            LRUCache<int, string> cache = new LRUCache<int, string>(5);
            try
            {
                cache.put(1, "This is the value");
                cache.put(2, "This is another value");
                cache.put(3, "This is another value");
                cache.put(4, "This is another value");
                cache.put(5, "This is another value");
                cache.put(7, "This is another value");
                cache.put(8, "This is another value");
                cache.put(8, "This is another value");
                cache.put(8, "This is another value");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}