using System.Collections;
using System.Collections.Generic;
using FilteringEnumerator;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void Test()
        {
            var nums = new List<int>() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}.AsReadOnly();
            var smallNums = new FilteringCollection<int>(nums, new LessThanFive());
            var bigNums = new FilteringCollection<int>(nums, new GreaterThanFive());
            Assert.That(smallNums, Is.All.LessThan(5));
            Assert.That(bigNums, Is.All.GreaterThan(5));
        }
    }

    /// <summary>
    /// A generic collection that uses a FilteringEnumerator
    /// </summary>
    internal class FilteringCollection<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _backingCollection;
        private readonly IObjectTest<T> _filter;

        public FilteringCollection(IEnumerable<T> collection, IObjectTest<T> filter)
        {
            _backingCollection = collection;
            _filter = filter;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new FilteringEnumerator<T>(_backingCollection.GetEnumerator(), _filter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class LessThanFive : IObjectTest<int>
    {
        public bool Test(int i)
        {
            return i < 5;
        }
    }

    internal class GreaterThanFive : IObjectTest<int>
    {
        public bool Test(int i)
        {
            return i > 5;
        }
    }
}
