// file was generated via Polyglottos Fluentator
// http://code.google.com/p/polyglottos/ by Pavel Savara

namespace System.Xml.Linq
{
    static public partial class XDocumentExtensions
    {
        static public demomodel.Library AddLibrary(this System.Xml.Linq.XDocument self, System.String id, System.Action<demomodel.Library> result = null)
        {
            demomodel.Library item = new demomodel.Library("library", id);
            self.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
}
