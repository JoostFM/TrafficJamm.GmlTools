using System.Xml.Linq;
using System.Xml.Serialization;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


Console.WriteLine("Converting XML to GmlModels...");

ConvertXmlToGmlModels();

void ConvertXmlToGmlModels()
{
    var xmlPath = "../../../data/20260515.GML.DbDump.xml";
    var xml = File.ReadAllText(xmlPath);
    
    var doc = XDocument.Parse(xml);
    var root = doc.Root;

    if (root == null) return;
    
    var gmlModel = DeserializeElement<GML>(root);
    Console.WriteLine(gmlModel.AppSpecificInfo.Length + " items");
    // Process each element based on your GmlModel structure
    //foreach (var element in gmlModel.AppSpecificInfo)
    //{
    //    Console.WriteLine($"Processing: {element.Name}");

    //    // Example: Deserialize to specific GmlModel type
    //     var gmlModel = DeserializeElement<GML>(element);
    //}
}

// Generic deserialization helper
T DeserializeElement<T>(XElement element) where T : class
{
    var serializer = new XmlSerializer(typeof(T));
    return serializer.Deserialize(element.CreateReader()) as T;
}
