using System.Collections.Generic;

namespace demomodel
{
    public class Model
    {
        public Model()
        {
            Companies=new List<Company>();
        }
        public List<Company> Companies { get; private set; }
    }

    public class Company
    {
        public Company(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public List<Department> Departments = new List<Department>();
    }

    public class Department
    {
        public Department()
        {
            Teams = new List<Team>();
        }

        public Department(string name, string id)
        {
            Teams = new List<Team>();
            Name = name;
            Id = id;
        }

        public Department(string name)
        {
            Teams = new List<Team>();
            Name = name;
        }

        public string Name { get; private set; }
        public string Id { get; private set; }

        public List<Team> Teams { get; private set; }
    }

    public class Team
    {
        public Team(string name)
        {
            Name = name;
            Employees=new List<Employee>();
        }

        public bool IsAwesome;
        public string Name { get; private set; }
        public List<Employee> Employees;
    }

    public class Employee
    {
        public Employee(string name)
        {
        }

        public string Name;
        public string Age;
    }
}
