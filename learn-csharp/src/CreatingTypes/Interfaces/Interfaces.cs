namespace CreatingTypes.Interfaces
{
    public interface IShape
    {
        double Area();
    }

    public class Circle : IShape
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public double Area()
        {
            return Math.PI * radius * radius;
        }
    }

    public class Rectangle : IShape
    {
        private double length;
        private double width;

        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        public double Area()
        {
            return length * width;
        }
    }

    public class Interface
    {
        public static void Run()
        {
            IShape circle = new Circle(5);
            Console.WriteLine($"Area of Circle: {circle.Area()}");

            IShape rectangle = new Rectangle(5, 10);
            Console.WriteLine($"Area of Rectangle: {rectangle.Area()}");
        }
    }
}