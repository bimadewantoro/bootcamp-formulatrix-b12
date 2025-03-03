namespace CreatingTypes.Inheritance
{
    public class Person
    {
        // Fields
        private string firstName;
        private string lastName;

        // Constructor
        public Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        // Properties
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        // Method
        public void DisplayFullName()
        {
            Console.WriteLine($"Full Name: {firstName} {lastName}");
        }
    }

    public class Employee : Person
    {
        // Fields
        private string department;

        // Constructor
        public Employee(string firstName, string lastName, string department)
            : base(firstName, lastName)
        {
            this.department = department;
        }

        // Property
        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        // Method
        public void DisplayDepartment()
        {
            Console.WriteLine($"Department: {department}");
        }
    }

    public class Inheritance
    {
        public static void Run()
        {
            // Creating an instance of the Employee class
            Employee employee = new Employee("John", "Doe", "IT");

            // Accessing properties
            Console.WriteLine(employee.FirstName);
            Console.WriteLine(employee.LastName);
            Console.WriteLine(employee.Department);

            // Calling methods
            employee.DisplayFullName();
            employee.DisplayDepartment();
        }
    }
}