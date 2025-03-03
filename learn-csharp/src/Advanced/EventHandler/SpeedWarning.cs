namespace Advanced.SpeedWarning
{
    public class SpeedEventArgs : EventArgs
    {
        public double CurrentSpeed { get; }

        public SpeedEventArgs(double currentSpeed)
        {
            CurrentSpeed = currentSpeed;
        }
    }

    public class Car
    {
        private double speed;
        public string Model { get; }

        public event EventHandler<SpeedEventArgs>? SpeedChanged;

        public Car(string model)
        {
            Model = model;
            speed = 0;
        }

        public double Speed
        {
            get { return speed; }
            set
            {
                if (speed != value)
                {
                    speed = value;
                    OnSpeedChanged(new SpeedEventArgs(speed));
                }
            }
        }

        protected virtual void OnSpeedChanged(SpeedEventArgs e)
        {
            SpeedChanged?.Invoke(this, e);
        }
    }

    public class SpeedMonitor
    {
        private readonly double speedLimit;

        public SpeedMonitor(double speedLimit)
        {
            this.speedLimit = speedLimit;
        }

        public void OnSpeedChanged(object? sender, SpeedEventArgs e)
        {
            if (sender is Car car)
            {
                if (e.CurrentSpeed > speedLimit)
                {
                    Console.WriteLine($"WARNING: {car.Model} is exceeding the speed limit! Current speed: {e.CurrentSpeed} km/h");
                }
                else if (e.CurrentSpeed > speedLimit * 0.9)
                {
                    Console.WriteLine($"CAUTION: {car.Model} is approaching the speed limit. Current speed: {e.CurrentSpeed} km/h");
                }
                else
                {
                    Console.WriteLine($"{car.Model} is traveling at {e.CurrentSpeed} mph");
                }
            }
        }
    }

    public class SpeedWarning
    {
        public static void Run()
        {
            SpeedMonitor monitor = new SpeedMonitor(100);

            Car car1 = new Car("Toyota Camry");
            Car car2 = new Car("Honda Civic");
            Car car3 = new Car("Ford Mustang");

            car1.SpeedChanged += monitor.OnSpeedChanged;
            car2.SpeedChanged += monitor.OnSpeedChanged;
            car3.SpeedChanged += monitor.OnSpeedChanged;

            car1.Speed = 80;
            car2.Speed = 110;
            car3.Speed = 120;

            car1.Speed = 90;
            car2.Speed = 100;
            car3.Speed = 110;

            car1.Speed = 100;
            car2.Speed = 90;
            car3.Speed = 80;
        }
    }
}