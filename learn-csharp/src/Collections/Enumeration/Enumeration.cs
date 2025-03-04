using System.Collections;

namespace Collections.Enumeration
{
    public class CustomCollection : IEnumerable
    {
        private string[] _items = { "Item1", "Item2", "Item3" };

        public IEnumerator GetEnumerator()
        {
            return new CustomEnumerator(_items);
        }
    }

    public class CustomEnumerator : IEnumerator
    {
        private string[] _items;
        private int _position = -1;

        public CustomEnumerator(string[] items)
        {
            _items = items;
        }

        public object Current
        {
            get
            {
                if (_position < 0 || _position >= _items.Length)
                    throw new InvalidOperationException();
                return _items[_position];
            }
        }

        public bool MoveNext()
        {
            _position++;
            return _position < _items.Length;
        }

        public void Reset()
        {
            _position = -1;
        }
    }

    public class CustomGenericCollection<T> : IEnumerable<T>
    {
        private T[] _items;

        public CustomGenericCollection(T[] items)
        {
            _items = items;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Enumeration
    {
        public static void Run()
        {
            CustomCollection collection = new CustomCollection();
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }

            CustomGenericCollection<int> genericCollection = new CustomGenericCollection<int>(new int[] { 1, 2, 3 });
            foreach (var item in genericCollection)
            {
                Console.WriteLine(item);
            }
        }
    }
}