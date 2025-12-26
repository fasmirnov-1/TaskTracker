using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

using TaskTracker.Extentions;

namespace TaskTracker.IO
{
    public class JSONConverter: IDisposable
    {
        public string Serialize(List<FileRecord> data)
        {
            return data.ToJson();
        }

        public List<FileRecord>Deserialize(string jsonContent)
        {
            string[] lineContentParts = jsonContent.ToContentParts();

            List<FileRecord> contentResult = lineContentParts.ConvertPartsToRecords();
        
            return contentResult;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
