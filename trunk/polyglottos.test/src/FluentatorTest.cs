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

            rf.GenerateFluentAPI(typeof(Model), @"..\..\DemoModel");
        }

        [Test]
        public void TestCompanyBuilder()
        {
            var model = new Model();
            model.AddCompany("Boldbrick & co.",
                bb =>
                    {
                        bb.AddDepartment("Software & Visions", "swv",
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

                        bb.AddDepartment("Office Management",
                            office => office.AddTeam("All hands",
                                jl =>
                                    {
                                        jl.AddEmployee("Jana");
                                        jl.AddEmployee("Lucka");
                                    }));
                    });

            Team visionsTeam = model.Companies[0].Departments.Where(dept => dept.Id == "swv").SelectMany(d => d.Teams).Single(t => t.Name == "Visions");
            Assert.IsTrue(visionsTeam.IsAwesome);
        }
    }
}