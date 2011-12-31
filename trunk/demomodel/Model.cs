#region Copyright (C) 2011 by Pavel Savara

/*
This file is part of polyglottos library - code generator tool
http://code.google.com/p/polyglottos/

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

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
            Name = name;
        }

        public string Name;
        public string Age;
    }
}
