using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace PasswordManagerV3.Other.Serialization.Serializers
{
    internal class DataContractXmlAnyTypeSerializer : IAnyTypeSerializer<XElement>
    {
        public XElement Serialize<T>(T obj)
        {
            var xmlBuilder = new StringBuilder();
            var writerSettings = new XmlWriterSettings {Indent = true};

            using (XmlWriter writer = XmlWriter.Create(xmlBuilder, writerSettings)) {
                new DataContractSerializer(typeof (T)).WriteObject(writer, obj);
                writer.Flush();
            }
            return XElement.Parse(xmlBuilder.ToString());
        }

        public T Deserialize<T>(XElement element)
        {
            using(var stringReader = new StringReader(element.ToString()))
            using (XmlReader reader = XmlReader.Create(stringReader)) {
                return (T)new DataContractSerializer(typeof (T)).ReadObject(reader);
            }
        }
    }
}