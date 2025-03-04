namespace Collections.StacksQueues
{
    public class Stacks
    {
        public static void Run()
        {
            UseBuiltInStack();

            UseCustomStack();
        }

        private static void UseBuiltInStack()
        {
            Console.WriteLine("--Built-in Stack--");
            Stack<string> customers = new Stack<string>();

            Console.WriteLine("Adding Customers:");
            customers.Push("Slamet");
            Console.WriteLine("  Added: Slamet");
            customers.Push("Joko");
            Console.WriteLine("  Added: Joko");
            customers.Push("Widodo");
            Console.WriteLine("  Added: Widodo");

            Console.WriteLine($"Number of customers in stack: {customers.Count}");

            Console.WriteLine($"Top customers: {customers.Peek()}");

            Console.WriteLine("\nRemoving customers:");
            Console.WriteLine($"  Removed: {customers.Pop()}");
            Console.WriteLine($"  Removed: {customers.Pop()}");
            Console.WriteLine($"  Removed: {customers.Pop()}");

            Console.WriteLine($"Stack is empty: {customers.Count == 0}");
        }

        private static void UseCustomStack()
        {
            Console.WriteLine("--Custom Stack--");
            CustomStack<int> numbers = new CustomStack<int>();

            numbers.Push(10);
            numbers.Push(20);
            numbers.Push(30);
            numbers.Push(40);

            Console.WriteLine($"Stack count: {numbers.Count}");
            Console.WriteLine($"Top item: {numbers.Peek()}");

            Console.WriteLine("\nProcessing all items:");
            while (numbers.Count > 0)
            {
                Console.WriteLine($"  Popped: {numbers.Pop()}");
            }

            Console.WriteLine($"Stack is empty: {numbers.Count == 0}");
        }
    }

    public class CustomStack<T>
    {
        private readonly List<T> _items = new List<T>();

        public int Count => _items.Count;

        public void Push(T item)
        {
            _items.Add(item);
        }

        public T Pop()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Stack is empty");

            int lastIndex = _items.Count - 1;
            T item = _items[lastIndex];
            _items.RemoveAt(lastIndex);
            return item;
        }

        public T Peek()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Stack is empty");

            return _items[_items.Count - 1];
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}