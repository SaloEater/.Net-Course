using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;
using System.IO;

namespace Core.ValueObject
{
    public class MicroserviceRouteConfiguration
    {
        private const string ROUTES_FILE_NAME = "routes.xml";
        private const string ROUTE_NOTE_TYPE = "route";
        private const string ROUTE_URI_ATTRIBUTE = "uri";
        private const string ROUTE_NAME_ATTRIBUTE = "name";

        public string uri { get; }
        public MicroserviceRouteConfiguration(string name)
        {
            XmlReaderSettings settings = new();
            settings.IgnoreWhitespace = true;

            using (var fileStream = File.OpenText(ROUTES_FILE_NAME))
            using (XmlReader reader = XmlReader.Create(fileStream, settings)) {
                while (reader.Read()) {
                    switch (reader.NodeType) {
                        case XmlNodeType.Element:
                            if (reader.Name == ROUTE_NOTE_TYPE && reader.GetAttribute(ROUTE_NAME_ATTRIBUTE) == name) {
                                uri = reader.GetAttribute(ROUTE_URI_ATTRIBUTE);
                                return;
                            }
                            break;
                    }
                }
            }
        }
    }
}
