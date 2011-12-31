// file was generated via Polyglottos Fluentator
// http://code.google.com/p/polyglottos/ by Pavel Savara

namespace demomodel
{
    static public partial class ModelExtensions
    {
        static public demomodel.Company AddCompany(this demomodel.Model self, demomodel.Company item, System.Action<demomodel.Company> result = null)
        {
            self.Companies.Add(item);
            if (result != null) result(item);
            return item;
        }
        
        static public System.Collections.Generic.IEnumerable<demomodel.Company> AddCompanys(this demomodel.Model self, System.Collections.Generic.IEnumerable<demomodel.Company> items, System.Action<demomodel.Company> result = null)
        {
            self.Companies.AddRange(items);
            if (result != null) foreach (var item in items) { result(item); };
            return items;
        }
        
        static public demomodel.Company AddCompany(this demomodel.Model self, System.String name, System.Action<demomodel.Company> result = null)
        {
            demomodel.Company item = new demomodel.Company(name);
            self.Companies.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
}
