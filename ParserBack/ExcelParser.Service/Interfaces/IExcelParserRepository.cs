using ExcelParser.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelParser.Service.Interfaces
{
    public interface IExcelParserRepository
    {
        public Task<IEnumerable<EntryModel>> GetEntries();
        public Task<int> InsertNew(List<EntryModel> entries);
    }
}
