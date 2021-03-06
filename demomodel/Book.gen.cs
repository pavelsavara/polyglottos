// file was generated via Polyglottos Fluentator
// http://code.google.com/p/polyglottos/ by Pavel Savara

namespace demomodel
{
    static public partial class BookExtensions
    {
        static public demomodel.Author AddAuthor(this demomodel.Book self, demomodel.Author item, System.Action<demomodel.Author> result = null)
        {
            self.Add(item);
            if (result != null) result(item);
            return item;
        }
        
        static public System.Collections.Generic.IEnumerable<demomodel.Author> AddAuthors(this demomodel.Book self, System.Collections.Generic.IEnumerable<demomodel.Author> items, System.Action<demomodel.Author> result = null)
        {
            foreach (var item in items) { self.Add(item); };
            if (result != null) foreach (var item in items) { result(item); };
            return items;
        }
        
        static public demomodel.Author AddAuthor(this demomodel.Book self, System.String name, System.Action<demomodel.Author> result = null)
        {
            demomodel.Author item = new demomodel.Author("author", name);
            self.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
    
    public partial class Book : global::System.Xml.Linq.XElement
    {
        public Book(string xelementname, System.String name)
            : base(System.Xml.Linq.XName.Get(xelementname,"http://polyglottos.googlecode.com/svn/trunk/demomodel/library.xsd"))
        {
            Add(new System.Xml.Linq.XAttribute(System.Xml.Linq.XName.Get("name"), name));
        }
    }
}
