using System.Collections;

namespace Collections.ArrayClass
{
    public class ArrayClass
    {
        public static void Run()
        {
            // Creating an array
            int[] numbers = new int[3];
            numbers[0] = 10;
            numbers[1] = 20;
            numbers[2] = 30;

            // Accessing elements
            Console.WriteLine(numbers[0]);
            Console.WriteLine(numbers[1]);
            Console.WriteLine(numbers[2]);

            // Iterating through an array
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }

            // Clone an array
            int[] clone = (int[])numbers.Clone();

            Console.WriteLine(clone[0]);

            // Multidimensional arrays
            int[,] matrix = new int[2, 2];
            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[1, 0] = 3;
            matrix[1, 1] = 4;

            // Jagged arrays
            int[][] jaggedArray = new int[2][];
            jaggedArray[0] = new int[] { 1, 2, 3 };
            jaggedArray[1] = new int[] { 4, 5, 6 };

            // Accessing elements
            Console.WriteLine(jaggedArray[0][0]);
            Console.WriteLine(jaggedArray[0][1]);
            Console.WriteLine(jaggedArray[0][2]);
            Console.WriteLine(jaggedArray[1][0]);
            Console.WriteLine(jaggedArray[1][1]);
            Console.WriteLine(jaggedArray[1][2]);
        }
    }
}