using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficJamm.GmlToFileSystem.Gml.Models;
using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem;

public class GmlApi
{
    public async Task<Result<string>> CreateTrackListFromXmlDbDumpGmlAsync(DbDumpRequest request)
    {
       // var dumpFilePath = await GetDbDumpFilePathAsync(request);
        var result = new Result<string>();

        return result;
    }
}