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

// ReSharper disable ConvertToLambdaExpression
// ReSharper disable PossibleMultipleEnumeration

using System.Collections.Generic;
using System.Linq;
using demomodel;
using NUnit.Framework;
using polyglottos.fluentator;

namespace polyglottos.test
{
    
    [TestFixture]
    public class ReflectionFluentatorTest
    {
        [Test]
        public void TestReflectionFluentator()
        {
            var rf=new ReflectionFluentator();

            rf.GenerateFluentAPI(typeof(Model), @"..\..\DemoModel");
        }

        [Test]
        public void OldCompanyBuilder()
        {
            var devNames = new List<string> {"Vasek", "Pasek"};

            var model = new Model();
            var pavel = new Employee("Pavel");
            model.Companies.Add(new Company("Boldbrick & co.")
            {
                Departments = new List<Department>
                {
                    new Department("Software & Visions", "swv")
                    {
                        Teams = new List<Team>
                        {
                            new Team("Visions")
                            {
                                Employees = new List<Employee>
                                {
                                    // I was forced to move 
                                    // pavel variable declaration completely out of scope
                                    pavel,
                                    new Employee("Ondra"),
                                },
                                IsAwesome = true,
                            },
                            new Team("Developers")
                            {
                                Employees = new List<Employee>
                                (
                                    // I can't do any statements or declarations here
                                    // to prepare my data in-place
                                    devNames.Select(n=>new Employee(n))
                                )
                                {
                                    // note I can't add pavel first
                                    pavel,
                                }
                            }
                        }
                    }
                }
            });
        }



        [Test]
        public void NewCompanyBuilder()
        {
            var devNames = new List<string> {"Vasek", "Pasek"};

            var model = new Model();
            model.AddCompany("Boldbrick & co.", bb =>
            {
                bb.AddDepartment("Software & Visions", "swv", swv =>
                {
                    var pavel = new Employee("pavel");
                    swv.AddTeam("Visions", visions =>
                    {
                        visions.IsAwesome = true;
                        visions.AddEmployee(pavel);
                        visions.AddEmployee("Ondra");
                    });
                    swv.AddTeam("Developers", devs => 
                    {
                        devs.AddEmployee(pavel);
                        // I can add more employees after Pavel
                        devs.AddEmployees(devNames.Select(n => new Employee(n)), dev=>
                        {
                            dev.Age = 33;
                        });
                        // and also can use any complex statement in-place
                        for (int i = 0; i < devNames.Count; i++)
                        {
                            int ix=i;
                            devs.AddEmployee(devNames[i], dev =>
                            {
                                dev.Age = ix;
                            });
                        }
                    });
                });
                bb.AddDepartment("Office Management", 
                    ot => ot.AddTeam("All hands", jl => 
                    {
                        jl.AddEmployee("Jana");
                        jl.AddEmployee("Lucka");
                    }));
            });

            var allTeams = model.Companies.SelectMany(c => c.Departments).SelectMany(d => d.Teams);
            Team visionsTeam = allTeams.Single(t => t.Name == "Visions");
            Assert.IsTrue(visionsTeam.IsAwesome);

            Team officeTeam = allTeams.Single(t => t.Name == "All hands");
            officeTeam.AddEmployee("Petra");
        }
    }
}