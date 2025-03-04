namespace Collections.StacksQueues
{
    public class Queues
    {
        public static void Run()
        {
            UseBuiltInQueue();

            UseCustomQueue();
        }

        private static void UseBuiltInQueue()
        {
            Console.WriteLine($"--Built-in Queue--");
            Queue<string> customers = new Queue<string>();

            Console.WriteLine("Enqueuing customers:");
            customers.Enqueue("Slamet");
            Console.WriteLine("  Added: Slamet");
            customers.Enqueue("Joko");
            Console.WriteLine("  Added: Joko");
            customers.Enqueue("Widodo");
            Console.WriteLine("  Added: Widodo");

            Console.WriteLine($"Number of customers in queue: {customers.Count}");

            Console.WriteLine($"Next customer to be served: {customers.Peek()}");

            Console.WriteLine("\nServing customers:");
            Console.WriteLine($"  Serving: {customers.Dequeue()}");
            Console.WriteLine($"  Serving: {customers.Dequeue()}");
            Console.WriteLine($"  Serving: {customers.Dequeue()}");

            Console.WriteLine($"Queue is empty: {customers.Count == 0}");
        }

        private static void UseCustomQueue()
        {
            Console.WriteLine($"--Custom Queue--");
            CustomQueue<int> numbers = new CustomQueue<int>();

            numbers.Enqueue(10);
            numbers.Enqueue(20);
            numbers.Enqueue(30);
            numbers.Enqueue(40);

            Console.WriteLine($"Queue count: {numbers.Count}");
            Console.WriteLine($"Next item: {numbers.Peek()}");

            Console.WriteLine("\nProcessing all items:");
            while (numbers.Count > 0)
            {
                Console.WriteLine($"  Dequeued: {numbers.Dequeue()}");
            }

            Console.WriteLine($"Queue is empty: {numbers.Count == 0}"); 
        }
    }

    public class CustomQueue<T>
    {
        private readonly List<T> _items = new List<T>();

        public int Count => _items.Count;

        public void Enqueue(T item)
        {
            _items.Add(item);
        }

        public T Dequeue()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T item = _items[0];
            _items.RemoveAt(0);
            return item;
        }

        public T Peek()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            return _items[0];
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