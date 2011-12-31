// file was generated via Polyglottos Fluentator
// http://code.google.com/p/polyglottos/ by Pavel Savara

namespace demomodel
{
    static public class CompanyExtensions
    {
        static public demomodel.Department AddDepartment(this demomodel.Company self, System.Action<demomodel.Department> result = null)
        {
            demomodel.Department item = new demomodel.Department();
            self.Departments.Add(item);
            if (result != null) result(item);
            return item;
        }
        
        static public demomodel.Department AddDepartment(this demomodel.Company self, System.String name, System.String id, System.Action<demomodel.Department> result = null)
        {
            demomodel.Department item = new demomodel.Department(name, id);
            self.Departments.Add(item);
            if (result != null) result(item);
            return item;
        }
        
        static public demomodel.Department AddDepartment(this demomodel.Company self, System.String name, System.Action<demomodel.Department> result = null)
        {
            demomodel.Department item = new demomodel.Department(name);
            self.Departments.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
}
