namespace System.Xml.Linq
{
    static public class XDocumentExtensions
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
