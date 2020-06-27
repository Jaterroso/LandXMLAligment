using System.IO;
using System.Xml.Serialization;

namespace Lib.LandXML
{
    public class Loader
    {
        public Xml2CSharp.LandXML Load(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Xml2CSharp.LandXML));
            
            using (FileStream fileStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (NamespaceIgnorantXmlTextReader objXmlReader = new NamespaceIgnorantXmlTextReader(fileStream))
                {
                    return (Xml2CSharp.LandXML)xmlSerializer.Deserialize(objXmlReader);
                }
            }
        }
    }
}
