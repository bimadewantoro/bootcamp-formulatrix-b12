namespace Advanced.EventManager
{
    public class EventManager
    {
        public static void Run()
        {
            var publisher = new Publisher();
            var subscriber = new Subscriber();

            publisher.MyEvent += subscriber.OnEvent;

            publisher.DoSomething();
        }
    }

    public class Publisher
    {
        public event EventHandler? MyEvent;

        public void DoSomething()
        {
            Console.WriteLine("Publisher: I'm doing something.");
            MyEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Subscriber
    {
        public void OnEvent(object? sender, EventArgs e)
        {
            Console.WriteLine("Subscriber: I'm reacting to the event.");
        }
    }
}