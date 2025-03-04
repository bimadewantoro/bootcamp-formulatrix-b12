namespace FrameworkFundamental.DateTimes
{
    public class DateTimes
    {
        public static void Run()
        {
            // Timespan
            var timeSpan = new TimeSpan(1, 2, 3);
            var timeSpan1 = TimeSpan.FromDays(1);
            Console.WriteLine("TimeSpan: " + timeSpan);
            Console.WriteLine("TimeSpan1: " + timeSpan1);

            var start = DateTime.Now;
            var end = DateTime.Now.AddMinutes(2);

            var duration = end - start;

            Console.WriteLine("Duration: " + duration);

            // Date Time
            var dateTime = new DateTime(2015, 1, 1);
            Console.WriteLine("DateTime: " + dateTime);

            var now = DateTime.Now;

            Console.WriteLine("Hour: " + now.Hour);
            Console.WriteLine("Minute: " + now.Minute);

            // Add minutes
            Console.WriteLine("Add Minutes: " + now.AddMinutes(2));
            Console.WriteLine("Add Minutes: " + now.AddHours(2));

            // Date time offset
            var offset = new DateTimeOffset(2015, 1, 1, 10, 0, 0, new TimeSpan(2, 0, 0));
            Console.WriteLine("Offset: " + offset);
        }
    }
}