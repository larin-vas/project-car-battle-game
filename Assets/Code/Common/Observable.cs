using System;

namespace Code.Common
{
    public class Observable<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChanged?.Invoke(value);
            }
        }

        public event Action<T> OnChanged;

        public Observable(T value)
        {
            _value = value;
        }

    }
}