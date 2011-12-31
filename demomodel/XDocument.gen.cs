// file was generated via Polyglottos Fluentator
// http://code.google.com/p/polyglottos/ by Pavel Savara

namespace System.Xml.Linq
{
    static public partial class XDocumentExtensions
    {
        static public demomodel.Library AddLibrary(this System.Xml.Linq.XDocument self, demomodel.Library item, System.Action<demomodel.Library> result = null)
        {
            self.Add(item);
            if (result != null) result(item);
            return item;
        }
        
        static public System.Collections.Generic.IEnumerable<demomodel.Library> AddLibrarys(this System.Xml.Linq.XDocument self, System.Collections.Generic.IEnumerable<demomodel.Library> items, System.Action<demomodel.Library> result = null)
        {
            foreach (var item in items) { self.Add(item); };
            if (result != null) foreach (var item in items) { result(item); };
            return items;
        }
        
        static public demomodel.Library AddLibrary(this System.Xml.Linq.XDocument self, System.String id, System.Action<demomodel.Library> result = null)
        {
            demomodel.Library item = new demomodel.Library("library", id);
            self.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
}
