using ApiUtilities.Models;
using ExcelParser.Service.Models;
using System.Collections.Generic;

namespace ExcelParser.Service
{
    public interface IExcelParserService
    {
        public List<EntryModel> GetEntries();
        public List<EntryModel> InsertTestData();
        public void UploadNew(MultiPartRequest docInfo);
    }
}
