// file was generated via Polyglottos Fluentator
// http://code.google.com/p/polyglottos/ by Pavel Savara

namespace demomodel
{
    static public class LibraryExtensions
    {
        static public demomodel.Book AddBook(this demomodel.Library self, System.String name, System.Action<demomodel.Book> result = null)
        {
            demomodel.Book item = new demomodel.Book("book", name);
            self.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
    
    public class Library : global::System.Xml.Linq.XElement
    {
        public Library(string xelementname, System.String id)
            : base(System.Xml.Linq.XName.Get(xelementname,"http://polyglottos.googlecode.com/svn/trunk/demomodel/library.xsd"))
        {
            Add(new System.Xml.Linq.XAttribute(System.Xml.Linq.XName.Get("id"), id));
        }
    }
}
