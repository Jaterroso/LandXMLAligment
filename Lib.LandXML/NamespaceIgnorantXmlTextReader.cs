using System.IO;
using System.Xml;

namespace Lib.LandXML
{
    public class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        public NamespaceIgnorantXmlTextReader(Stream reader) : base(reader) { }

        public override string NamespaceURI
        {
            get { return string.Empty; }
        }
    }
}