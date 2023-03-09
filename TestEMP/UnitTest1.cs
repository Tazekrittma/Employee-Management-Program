using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class ProgramTests
{
    [Test]
    public void TestOrgChart()
    {
        // Arrange 
        List<Employee> employees = new List<Employee>()
        {
            new Employee("John Smith", "CEO", null),
            new Employee("Jane Doe", "VP of Marketing", "John Smith"),
            new Employee("Bob Johnson", "VP of Sales", "John Smith"),
            new Employee("Sue Lee", "Marketing Manager", "Jane Doe"),
            new Employee("Jack Brown", "Sales Manager", "Bob Johnson"),
            new Employee("Jim Green", "Marketing Specialist", "Sue Lee"),
            new Employee("Jill Black", "Sales Representative", "Jack Brown")
        };

        // Act 
        string orgChart = Program.orgChart(employees);

        // Assert 
        StringAssert.Contains("John Smith (CEO)", orgChart);
        StringAssert.Contains("Bob Johnson (VP of Sales)", orgChart);
        StringAssert.Contains("Jack Brown (Sales Manager)", orgChart);
        StringAssert.Contains("Sue Lee (Marketing Manager)", orgChart);
        StringAssert.Contains("Jim Green (Marketing Specialist)", orgChart);
        StringAssert.Contains("Jill Black (Sales Representative)", orgChart);
    }
    [Test]
    public void TestAddEmployee()
    {
        // Arrange
        List<Employee> employees = new List<Employee>()
        {
            new Employee("John Smith", "CEO", null),
            new Employee("Jane Doe", "VP of Marketing", "John Smith"),
            new Employee("Bob Johnson", "VP of Sales", "John Smith")
        };

        // Act
        Program.addEmployee(employees, "Sue Lee", "Marketing Manager", "Jane Doe");

        // Assert
        Assert.AreEqual(4, employees.Count);
        StringAssert.Contains("Sue Lee - Marketing Manager", employees[3].ToString());
        StringAssert.Contains("Jane Doe", employees[3].Manager);
    }

    [Test]
    public void TestCheckDuplicates()
    {
        // Arrange
        List<Employee> employees = new List<Employee>()
        {
            new Employee("John Smith", "CEO", null),
            new Employee("Jane Doe", "VP of Marketing", "John Smith"),
            new Employee("Bob Johnson", "VP of Sales", "John Smith"),
            new Employee("Jane Doe", "Marketing Manager", "John Smith")
        };

        // Act
        List<Employee> newEmployees = Program.checkDuplicates(employees, true);

        // Assert
        Assert.AreEqual(3, newEmployees.Count);
        StringAssert.Contains("Jane Doe - VP of Marketing", newEmployees[1].ToString());
    }

    [Test]
    public void TestCheckEmployeeExists()
    {
        // Arrange
        List<Employee> employees = new List<Employee>()
        {
            new Employee("John Smith", "CEO", null),
            new Employee("Jane Doe", "VP of Marketing", "John Smith"),
            new Employee("Bob Johnson", "VP of Sales", "John Smith")
        };

        // Act
        bool employeeExists1 = Program.checkEmployeeExists(employees, "John Smith");
        bool employeeExists2 = Program.checkEmployeeExists(employees, "Sue Lee");

        // Assert
        Assert.IsTrue(employeeExists1);
        Assert.IsFalse(employeeExists2);
    }

    [Test]
    public void TestPrintAllEmployees()
    {
        // Arrange
        List<Employee> employees = new List<Employee>()
        {
            new Employee("John Smith", "CEO", null),
            new Employee("Jane Doe", "VP of Marketing", "John Smith"),
            new Employee("Bob Johnson", "VP of Sales", "John Smith"),
            new Employee("Sue Lee", "Marketing Manager", "Jane Doe")
        };

        // Act
        using (var consoleOutput = new ConsoleOutput())
        {
            Program.printAllEmployees(employees);
            string output = consoleOutput.GetOutput();

            // Assert
            StringAssert.Contains("All Employees:", output);
            StringAssert.Contains("John Smith - CEO", output);
            StringAssert.Contains("Jane Doe - VP of Marketing", output);
            StringAssert.Contains("Bob Johnson - VP of Sales", output);
            StringAssert.Contains("Sue Lee - Marketing Manager", output);
        }
    }
}
