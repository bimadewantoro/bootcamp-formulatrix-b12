namespace Advanced.EmployeePromotion
{
    public class Employee
    {
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public int Salary { get; set; }

        public Employee(string name, int yearsOfExperience, int salary)
        {
            Name = name;
            YearsOfExperience = yearsOfExperience;
            Salary = salary;
        }
    }

    public class EmployeePromotion
    {
        public delegate bool IsPromotable(Employee employee);

        public static void PromoteEmployee(Employee[] employees, IsPromotable isPromotable)
        {
            foreach (var employee in employees)
            {
                if (isPromotable(employee))
                {
                    Console.WriteLine($"{employee.Name} is promoted.");
                }
            }
        }

        public static void Run()
        {
            Employee[] employees = new Employee[]
            {
                new Employee("Steven", 5, 50000),
                new Employee("Roger", 2, 30000),
                new Employee("Cania", 3, 40000),
                new Employee("Stella", 1, 20000),
                new Employee("John", 4, 45000)
            };

            PromoteEmployee(employees, emp => emp.YearsOfExperience >= 3);
        }
    }
}