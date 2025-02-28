namespace CreatingTypes.Classes
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

    public class Classes
    {
        public static void Run()
        {
            // Creating an instance of the Person class
            Person person = new Person("John", "Doe");

            // Accessing properties
            Console.WriteLine(person.FirstName);
            Console.WriteLine(person.LastName);

            // Calling a method
            person.DisplayFullName();
        }
    }
}