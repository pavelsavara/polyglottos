// file was generated via Polyglottos Fluentator
// http://code.google.com/p/polyglottos/ by Pavel Savara

namespace demomodel
{
    static public partial class TeamExtensions
    {
        static public demomodel.Employee AddEmployee(this demomodel.Team self, demomodel.Employee item, System.Action<demomodel.Employee> result = null)
        {
            self.Employees.Add(item);
            if (result != null) result(item);
            return item;
        }
        
        static public System.Collections.Generic.IEnumerable<demomodel.Employee> AddEmployees(this demomodel.Team self, System.Collections.Generic.IEnumerable<demomodel.Employee> items, System.Action<demomodel.Employee> result = null)
        {
            self.Employees.AddRange(items);
            if (result != null) foreach (var item in items) { result(item); };
            return items;
        }
        
        static public demomodel.Employee AddEmployee(this demomodel.Team self, System.String name, System.Action<demomodel.Employee> result = null)
        {
            demomodel.Employee item = new demomodel.Employee(name);
            self.Employees.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
}
