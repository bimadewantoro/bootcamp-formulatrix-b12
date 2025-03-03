namespace Advanced.FireAlarm
{
    public class FireAlarm
    {
        public delegate void FireEventHandler(object sender, FireEventArgs e);

        public event FireEventHandler? FireEvent;

        public void OnFireEvent(string location)
        {
            FireEvent?.Invoke(this, new FireEventArgs(location));
        }

        public void RaiseFireAlarm(string location)
        {
            OnFireEvent(location);
        }

        public static void Run()
        {
            FireAlarm fireAlarm = new FireAlarm();
            FireAlarmListener listener = new FireAlarmListener(fireAlarm);

            fireAlarm.RaiseFireAlarm("Building A");
            fireAlarm.RaiseFireAlarm("Building B");
        }
    }

    public class FireEventArgs
    {
        public string Location { get; set; }

        public FireEventArgs(string location)
        {
            Location = location;
        }
    }

    public class FireAlarmListener
    {
        public FireAlarmListener(FireAlarm fireAlarm)
        {
            fireAlarm.FireEvent += FireAlarmHandler;
        }

        public void FireAlarmHandler(object sender, FireEventArgs e)
        {
            Console.WriteLine($"Fire reported at {e.Location}");
        }
    }
}