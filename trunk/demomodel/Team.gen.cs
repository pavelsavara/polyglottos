// file was generated via Polyglottos Fluentator
// http://code.google.com/p/polyglottos/ by Pavel Savara

namespace demomodel
{
    static public class TeamExtensions
    {
        static public demomodel.Employee AddEmployee(this demomodel.Team self, System.String name, System.Action<demomodel.Employee> result = null)
        {
            demomodel.Employee item = new demomodel.Employee(name);
            self.Employees.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
}
