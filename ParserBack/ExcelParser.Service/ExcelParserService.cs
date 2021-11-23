using ApiUtilities.Models;
using ExcelDataReader;
using ExcelParser.Service.Interfaces;
using ExcelParser.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace ExcelParser.Service
{
    public class ExcelParserService : IExcelParserService
    {
        private readonly IExcelParserRepository _repository;

        public ExcelParserService(IExcelParserRepository excelParserRepository)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _repository = excelParserRepository;
        }

        public List<EntryModel> GetEntries()
        {
            return _repository.GetEntries().Result.ToList();
        }

        public void UploadNew(MultiPartRequest docInfo)
        {
            var newEntries = new List<EntryModel>();
            using (var reader = ExcelReaderFactory.CreateReader(docInfo.FileStream))
            {
                bool isFirstRow = true;
                //each row
                while (reader.Read())
                {
                    if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue;
                    }

                    newEntries.Add(new EntryModel
                    {
                        FirstName = reader.GetValue(0).ToString(),
                        LastName = reader.GetValue(1).ToString(),
                        Age = int.Parse(reader.GetValue(2).ToString()),
                        Email = reader.GetValue(3).ToString(),
                        Ssn = reader.GetValue(4).ToString()
                    });
                }
            }

            _repository.InsertNew(newEntries);
        }

        public List<EntryModel> InsertTestData()
        {
            var entries = new List<EntryModel>()
            {
                new EntryModel{
                    FirstName = "Test",
                    LastName = "User",
                    Age = 22,
                    Email = "testUser@gmail.com",
                    Phone = "555-987-6543",
                    Ssn = "987-65-4321"
                },
                new EntryModel{
                    FirstName = "Paul",
                    LastName = "Lindemuth",
                    Age = 28,
                    Email = "paul.h.lindemuth@gmail.com",
                    Phone = "444-222-3456",
                    Ssn = "123-45-6789"
                }
            };
            _repository.InsertNew(entries);
            return entries;
        }
    }
}
