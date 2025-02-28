namespace Advanced.ArrayOfDelegates
{
    public class Operation
    {
        public static int Add(int x, int y)
        {
            return x + y;
        }

        public static int Subtract(int x, int y)
        {
            return x - y;
        }

        public static int Multiply(int x, int y)
        {
            return x * y;
        }

        public static int Divide(int x, int y)
        {
            return x / y;
        }
    }

    public class ArrayOfDelegates
    {
        public delegate int OperationDelegate(int x, int y);

        public static void Run()
        {
            OperationDelegate[] operations =
            [
                Operation.Add,
                Operation.Subtract,
                Operation.Multiply,
                Operation.Divide
            ];

            foreach (OperationDelegate operation in operations)
            {
                int result = operation(10, 5);
                Console.WriteLine("Method: {0}, Result: {1}", operation.Method.Name, result);
            }
        }
    }
}