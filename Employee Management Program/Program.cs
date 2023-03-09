using System;
using System.Collections.Generic;

public class Employee
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string ReportsTo { get; set; }

    public Employee(string name, string title, string reportsTo)
    {
        Name = name;
        Title = title;
        ReportsTo = reportsTo;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Employee> employees = new List<Employee>()
        {
            new Employee("John Smith", "CEO", null),
            new Employee("Jane Doe", "VP of Marketing", "John Smith"),
            new Employee("Bob Johnson", "VP of Sales", "John Smith"),
            new Employee("Sue Lee", "Marketing Manager", "Jane Doe"),
            new Employee("Mike Brown", "Sales Manager", "Bob Johnson"),
            new Employee("Tom Wilson", "Sales Representative", "Mike Brown"),
            new Employee("Kim Nguyen", "Marketing Coordinator", "Sue Lee")
        };

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1 - View org chart");
            Console.WriteLine("2 - Add new employee");
            Console.WriteLine("3 - Check for duplicates");
            Console.WriteLine("4 - Check if employee exists");
            Console.WriteLine("5 - View all employees");
            Console.WriteLine("6 - Close console");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    orgChart(employees);
                    break;
                case "2":
                    addEmployee(employees);
                    break;
                case "3":
                    checkDuplicates(employees);
                    break;
                case "4":
                    checkEmployeeExists(employees);
                    break;
                case "5":
                    printAllEmployees(employees);
                    break;
                case "6":
                    CloseConsole();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }
        }
    }

    static void orgChart(List<Employee> employees)
    {
        string chart = "";
        Dictionary<string, int> levels = new Dictionary<string, int>();

        foreach (Employee employee in employees)
        {
            if (employee.ReportsTo == null)
            {
                chart += $"{employee.Name} - {employee.Title}\n";
                levels[employee.Name] = chart.Length;
            }
            else
            {
                Employee boss = employees.Find(emp => emp.Name == employee.ReportsTo);
                int bossIndex = levels[boss.Name];
                chart = chart.Substring(0, bossIndex) + $"|_ {employee.Name} - {employee.Title}\n" + chart.Substring(bossIndex);
                levels[employee.Name] = bossIndex + 3;
            }
        }

        Console.WriteLine(chart);
    }

    static void addEmployee(List<Employee> employees)
    {
        Console.Write("\nAdd new employee? (y/n): ");
        string response = Console.ReadLine().ToLower();

        while (response == "y")
        {
            Console.Write("\nEnter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            Console.Write("Enter manager's name (or leave blank if reporting to CEO): ");
            string reportsTo = Console.ReadLine();

            employees.Add(new Employee(name, title, reportsTo));
            orgChart(employees);

            Console.Write("\nAdd another employee? (y/n): ");
            response = Console.ReadLine().ToLower();
        }
    }


    static void checkDuplicates(List<Employee> employees)
    {
        List<Employee> duplicates = new List<Employee>();
        HashSet<string> names = new HashSet<string>();

        foreach (Employee employee in employees)
        {
            if (!names.Add(employee.Name))
            {
                duplicates.Add(employee);
            }
        }

        if (duplicates.Count > 0)
        {
            Console.WriteLine("\nDuplicates found:\n");

            foreach (Employee employee in duplicates)
            {
                Console.WriteLine($"{employee.Name} - {employee.Title}");
            }

            Console.Write("\nWould you like to delete the duplicates? (y/n): ");
            string response = Console.ReadLine().ToLower();

            if (response == "y")
            {
                List<Employee> uniqueEmployees = new List<Employee>();
                HashSet<string> uniqueNames = new HashSet<string>();

                foreach (Employee employee in employees)
                {
                    if (uniqueNames.Add(employee.Name))
                    {
                        uniqueEmployees.Add(employee);
                    }
                }

                Console.WriteLine("\nDuplicates deleted. Here is the updated org chart:\n");
                orgChart(uniqueEmployees);
            }
            else
            {
                Console.WriteLine("\nDuplicates not deleted. Here is the original org chart:\n");
                orgChart(employees);
            }
        }
        else
        {
            Console.WriteLine("\nNo duplicates found.");
        }
    }


    static void checkEmployeeExists(List<Employee> employees)
    {
        Console.Write("\nEnter the name of the employee to check: ");
        string employeeName = Console.ReadLine();
        Employee employee = employees.Find(e => e.Name == employeeName);

        if (employee != null)
        {
            Console.WriteLine($"\n{employee.Name} - {employee.Title} exists in the org chart.");
            Console.Write("\nWould you like to add this employee to a new list? (y/n): ");
            string response = Console.ReadLine().ToLower();

            if (response == "y")
            {
                List<Employee> list2 = new List<Employee>() { employee };
                Console.WriteLine($"\n{employee.Name} - {employee.Title} added to list2.");
            }
            else
            {
                Console.WriteLine("\nEmployee not added to list2.");
            }
        }
        else
        {
            Console.WriteLine($"\n{employeeName} does not exist in the org chart.");
        }
    }


    static void printAllEmployees(List<Employee> employees)
    {
        Console.WriteLine("\nAll Employees:");
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees in the org chart.");
        }
        else
        {
            foreach (Employee employee in employees)
            {
                Console.WriteLine($"{employee.Name} - {employee.Title}");
            }
        }

        List<Employee> list2 = new List<Employee>();

        // add any employees from list2 to the main employees list
        if (list2.Count > 0)
        {
            employees.AddRange(list2);
            Console.WriteLine("\nEmployees added from list2:");
            foreach (Employee employee in list2)
            {
                Console.WriteLine($"{employee.Name} - {employee.Title}");
            }
        }
        else
        {
            Console.WriteLine("\nlist2 is empty.");
        }
    }

    static void CloseConsole()
    {
        Environment.Exit(0);
    }

}
