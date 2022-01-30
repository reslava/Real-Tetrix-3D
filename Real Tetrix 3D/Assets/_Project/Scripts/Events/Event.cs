using System;

namespace RafaEslava
{
    public class Event 
    {
        private event Action action = delegate{};

        public void Invoke()
        {
            action.Invoke();
        }

        public void AddListener(Action listener)
        {
            action += listener;
        }

        public void RemoveListener(Action listener)
        {
            action -= listener;
        }
    }

    public class Event<T> 
    {
        private event Action<T> action = delegate{};

        public void Invoke(T param)
        {
            action.Invoke(param);
        }

        public void AddListener(Action<T> listener)
        {
            action += listener;
        }

        public void RemoveListener(Action<T> listener)
        {
            action -= listener;
        }
    }

}
