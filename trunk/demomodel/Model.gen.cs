namespace demomodel
{
    static public class ModelExtensions
    {
        static public demomodel.Company AddCompany(this demomodel.Model self, System.String name, System.Action<demomodel.Company> result = null)
        {
            demomodel.Company item = new demomodel.Company(name);
            self.Companies.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
}
