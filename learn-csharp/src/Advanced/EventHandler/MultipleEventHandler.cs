namespace Advanced.MultipleEvenHandler
{
    // Define a delegate
    public delegate void MyEventHandler(object sender, MyEventArgs e);

    public class MyEventArgs : EventArgs
    {
        public string Message { get; set; }

        public MyEventArgs(string message)
        {
            Message = message;
        }
    }

    public class Publisher
    {
        public event MyEventHandler MyEvent;

        public void DoSomething()
        {
            Console.WriteLine("Publisher: I'm doing something.");
            MyEvent?.Invoke(this, new MyEventArgs("Event triggered."));
        }
    }

    public class Subscriber1
    {
        public void OnEvent(object sender, MyEventArgs e)
        {
            Console.WriteLine("Subscriber1: I'm reacting to the event.");
            Console.WriteLine("Subscriber1: Message: {0}", e.Message);
        }
    }

    public class Subscriber2
    {
        public void OnEvent(object sender, MyEventArgs e)
        {
            Console.WriteLine("Subscriber2: I'm reacting to the event.");
            Console.WriteLine("Subscriber2: Message: {0}", e.Message);
        }
    }

    public class MultipleEventHandler
    {
        public static void Run()
        {
            var publisher = new Publisher();
            var subscriber1 = new Subscriber1();
            var subscriber2 = new Subscriber2();

            publisher.MyEvent += subscriber1.OnEvent;
            publisher.MyEvent += subscriber2.OnEvent;

            publisher.DoSomething();
        }
    }
}