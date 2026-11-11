using GmlToFileSystem.Library.Gml.Models;
using GmlToFileSystem.Library.Gml.Utils;
using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;
using System.Xml;
using System.Xml.Serialization;

namespace GmlToFileSystem.Library.Gml.Workers;

[ExcludeFromCodeCoverage]
public class GmlItemReader(IFileSystem fileSystem) : IGmlItemReader
{
    public async Task<List<GMLDatabaseItem>> ReadGmlAudioItemsAsync(string gmlFilePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(gmlFilePath);

        var settings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Prohibit,
            XmlResolver = null,
            Async = true
        };

        await using var stream = fileSystem.File.OpenRead(gmlFilePath);
        using XmlReader reader = XmlReader.Create(stream, settings);
        XmlSerializer serializer = new(typeof(GmlModel));

        // Check if the root element can be deserialized
        if (!serializer.CanDeserialize(reader))
        {
            throw new InvalidOperationException("XML structure does not match expected GML format");
        }

        if (serializer.Deserialize(reader) is not GmlModel gmlModel)
        {
            throw new InvalidOperationException("Failed to deserialize the GML model from the XML file.");
        }

        if (gmlModel.AppSpecificInfo == null || gmlModel.AppSpecificInfo.Length == 0)
        {
            throw new InvalidOperationException("Xml contains no items in the AppSpecificInfo section.");
        }

        await Task.CompletedTask;

        return [.. gmlModel.AppSpecificInfo.FilterAudioItems()];
    }
}
