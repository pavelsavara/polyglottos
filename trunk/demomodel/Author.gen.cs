namespace demomodel
{
    static public class AuthorExtensions
    {
    }
    
    public class Author : global::System.Xml.Linq.XElement
    {
        public Author(string xelementname, System.String name)
            : base(System.Xml.Linq.XName.Get(xelementname,"http://polyglottos.googlecode.com/svn/trunk/demomodel/library.xsd"))
        {
            Add(new System.Xml.Linq.XAttribute(System.Xml.Linq.XName.Get("name"), name));
        }
    }
}