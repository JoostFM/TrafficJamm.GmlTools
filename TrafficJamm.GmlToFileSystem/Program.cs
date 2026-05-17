using System.Xml;
using System.Xml.Serialization;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


Console.WriteLine("Converting XML to GmlModels...");

ConvertXmlToGmlModels();

void ConvertXmlToGmlModels()
{
    try
    {
        var xmlPath = "../../../data/20260515.GML.DbDump.xml";

        using XmlReader reader = XmlReader.Create(xmlPath);
        XmlSerializer serializer = new(typeof(GML));

        // Check if the root element can be deserialized
        if (!serializer.CanDeserialize(reader))
        {
            Console.WriteLine("XML structure does not match expected GML format");
            return;
        }

        if (serializer.Deserialize(reader) is not GML gmlModel)
        {
            Console.WriteLine("XML structure does not match expected GML format");
            return;
        }

        Console.WriteLine($"Successfully loaded: {gmlModel.AppSpecificInfo?.Length ?? 0} items");

        // Process each database item
        if (gmlModel.AppSpecificInfo == null)
        {
            return;
        }

        foreach (var item in gmlModel.AppSpecificInfo)
        {
            Console.WriteLine($"Processing item: {item}");
            // Add your processing logic here
        }
    }
    catch (XmlException xmlEx)
    {
        Console.WriteLine($"XML parsing error: {xmlEx.Message}");
        Console.WriteLine($"Line: {xmlEx.LineNumber}, Position: {xmlEx.LinePosition}");
    }
    catch (InvalidOperationException invEx)
    {
        Console.WriteLine($"Deserialization error: {invEx.Message}");
        if (invEx.InnerException != null)
        {
            Console.WriteLine($"Inner exception: {invEx.InnerException.Message}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
