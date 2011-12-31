// file was generated via Polyglottos Fluentator
// http://code.google.com/p/polyglottos/ by Pavel Savara

namespace demomodel
{
    static public partial class DepartmentExtensions
    {
        static public demomodel.Team AddTeam(this demomodel.Department self, demomodel.Team item, System.Action<demomodel.Team> result = null)
        {
            self.Teams.Add(item);
            if (result != null) result(item);
            return item;
        }
        
        static public System.Collections.Generic.IEnumerable<demomodel.Team> AddTeams(this demomodel.Department self, System.Collections.Generic.IEnumerable<demomodel.Team> items, System.Action<demomodel.Team> result = null)
        {
            self.Teams.AddRange(items);
            if (result != null) foreach (var item in items) { result(item); };
            return items;
        }
        
        static public demomodel.Team AddTeam(this demomodel.Department self, System.String name, System.Action<demomodel.Team> result = null)
        {
            demomodel.Team item = new demomodel.Team(name);
            self.Teams.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
}
