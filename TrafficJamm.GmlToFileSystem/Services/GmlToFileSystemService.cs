using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficJamm.GmlToFileSystem.Services
{
    public class GmlToFileSystemService
    {
        public async Task<Models.Result> CopyFiles(string gmlFilePath, string outputDirectory)
        {
            var processor = new Process.GmlToFileSystemProcessor();
            return await processor.ProcessAsync(gmlFilePath, outputDirectory);
        }
    }
}
