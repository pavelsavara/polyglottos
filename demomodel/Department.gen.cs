// file was generated via Polyglottos Fluentator
// http://code.google.com/p/polyglottos/ by Pavel Savara

namespace demomodel
{
    static public class DepartmentExtensions
    {
        static public demomodel.Team AddTeam(this demomodel.Department self, System.String name, System.Action<demomodel.Team> result = null)
        {
            demomodel.Team item = new demomodel.Team(name);
            self.Teams.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
}
