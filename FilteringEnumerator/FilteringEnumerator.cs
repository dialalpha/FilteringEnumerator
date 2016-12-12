using System;
using System.Collections;
using System.Collections.Generic;

namespace FilteringEnumerator
{
    public sealed class FilteringEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _internalEnumerator;
        private readonly IObjectTest<T> _filter;

        public FilteringEnumerator(IEnumerator<T> enumerator, IObjectTest<T> filter )
        {
            if (enumerator == null || filter == null)
            {
                throw new ArgumentNullException(enumerator == null ? nameof(enumerator) : nameof(filter));
            }

            _internalEnumerator = enumerator;
            _filter = filter;
        }

        public bool MoveNext()
        {
            while (_internalEnumerator.MoveNext())
            {
                if (_filter.Test(_internalEnumerator.Current))
                {
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            _internalEnumerator.Reset();
        }

        public T Current => _internalEnumerator.Current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _internalEnumerator.Dispose();
        }
    }
}
