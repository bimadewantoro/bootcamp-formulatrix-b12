namespace FrameworkFundamental.EqualityComparison
{
    public class EqualityComparison
    {
        public static void Run()
        {
            int i = 42;
            int j = 42;

            Console.WriteLine(i == j);

            string s1 = "hello";
            string s2 = "hello";

            Console.WriteLine(s1 == s2);

            string s3 = new string(new char[] { 'h', 'e', 'l', 'l', 'o' });
            string s4 = new string(new char[] { 'h', 'e', 'l', 'l', 'o' });

            Console.WriteLine(s3 == s4);

            string s5 = "hello";
            string s6 = "HELLO";

            Console.WriteLine(s5 == s6);
            Console.WriteLine(s5.Equals(s6, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(s5 != s6);
            Console.WriteLine(!s5.Equals(s6, StringComparison.OrdinalIgnoreCase));
        }
    }
}