using DatabaseUtility;
using ExcelParser.Service.Interfaces;
using ExcelParser.Service.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcelParser.Service.Repositories
{
    public class ExcelParserRepository : IExcelParserRepository
    {
        private readonly IMongoCollection<EntryModel> _entries;
        public ExcelParserRepository(IDatabaseConnector dbConnection)
        {
            var client = new MongoClient(dbConnection.ConnectionString);
            var db = client.GetDatabase(dbConnection.DatabaseName);

            _entries =  db.GetCollection<EntryModel>(dbConnection.CollectionName);
        }

        public async Task<IEnumerable<EntryModel>> GetEntries()
        {
            return await _entries.Find(x => true).ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> InsertNew(List<EntryModel> entries)
        {
            await _entries.InsertManyAsync(entries).ConfigureAwait(false);
            return entries.Count;
        }
    }
}
