namespace Advanced.ObserverDesignPattern
{
    public class ObserverDesignPattern
    {
        public static void Run()
        {
            var weatherStation = new WeatherStation();
            var phoneDisplay = new PhoneDisplay();
            var windowDisplay = new WindowDisplay();

            weatherStation.Attach(phoneDisplay);
            weatherStation.Attach(windowDisplay);

            weatherStation.SetTemperature(25);
            weatherStation.SetTemperature(30);
        }
    }

    public class WeatherStation
    {
        private List<IObserver> _observers = new List<IObserver>();
        private int _temperature;

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_temperature);
            }
        }

        public void SetTemperature(int temperature)
        {
            _temperature = temperature;
            Console.WriteLine($"WeatherStation: New temperature is {_temperature} degrees.");
            Notify();
        }
    }

    public interface IObserver
    {
        void Update(int temperature);
    }

    public class PhoneDisplay : IObserver
    {
        public void Update(int temperature)
        {
            Console.WriteLine($"PhoneDisplay: The temperature is now {temperature} degrees.");
        }
    }

    public class WindowDisplay : IObserver
    {
        public void Update(int temperature)
        {
            Console.WriteLine($"WindowDisplay: The temperature is now {temperature} degrees.");
        }
    }
}