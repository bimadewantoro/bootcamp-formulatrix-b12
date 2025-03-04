namespace FrameworkFundamental.ConversionMechanisms
{
    public class ConversionMechanisms
    {
        public static void Run()
        {
            // Implicit conversion
            int i = 42;
            double d = i;

            Console.WriteLine(d);

            // Explicit conversion
            double dd = 42.0;
            int ii = (int)dd;

            Console.WriteLine(ii);

            // Convert class
            string s = "42";
            int j = Convert.ToInt32(s);

            Console.WriteLine(j);

            // Parse
            string ss = "42";
            int jj = int.Parse(ss);

            Console.WriteLine(jj);

            // TryParse
            string sss = "42";
            int jjj;
            if (int.TryParse(sss, out jjj))
            {
                Console.WriteLine(jjj);
            }
            else
            {
                Console.WriteLine("Failed to parse");
            }
        }
    }
}