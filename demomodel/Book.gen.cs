namespace demomodel
{
    static public class BookExtensions
    {
        static public demomodel.Author AddAuthor(this demomodel.Book self, System.String name, System.Action<demomodel.Author> result = null)
        {
            demomodel.Author item = new demomodel.Author("author", name);
            self.Add(item);
            if (result != null) result(item);
            return item;
        }
    }
    
    public class Book : global::System.Xml.Linq.XElement
    {
        public Book(string xelementname, System.String name)
            : base(System.Xml.Linq.XName.Get(xelementname))
        {
            Add(new System.Xml.Linq.XAttribute(System.Xml.Linq.XName.Get("name"), name));
        }
    }
}
