using System.Collections.Generic;
using System.IO;
using System.Linq;
using demomodel;
using NUnit.Framework;

namespace polyglottos.test.src
{
    
    [TestFixture]
    public class FluentatorTest
    {
        [Test]
        public void TestReflectionFluentator()
        {
            var rf=new ReflectionFluentator();

            rf.GenerateFluentAPI(typeof(Company), @"..\..\DemoModel");
        }

        [Test]
        public void TestCompanyBuilder()
        {
            var company = new Company("Boldbrick & co.");
            company.AddDepartment("Software & Visions", "swv",
                swv =>
                {
                    swv.AddTeam("Visions",
                        visions =>
                        {
                            visions.IsAwesome = true;
                            visions.AddEmployee("Pavel");
                            visions.AddEmployee("Ondra");
                        });
                    swv.AddTeam("Developers",
                        devs =>
                        {
                            devs.AddEmployee("Vasek");
                            devs.AddEmployee("Pasek");
                        });
                });

            company.AddDepartment("Office Management",
                office => office.AddTeam("All hands",
                    jj =>
                    {
                        jj.AddEmployee("Jana");
                        jj.AddEmployee("Lucka");
                    }));

            Team visionsTeam = company.Departments.Where(dept => dept.Id == "swv").SelectMany(d => d.Teams).Single(t => t.Name == "Visions");
            Assert.IsTrue(visionsTeam.IsAwesome);
        }
    }
}