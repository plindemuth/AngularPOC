using MongoDB.Bson.Serialization.Attributes;

namespace ExcelParser.Service.Models
{
    public class EntryModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Ssn { get; set; }
    }
}
