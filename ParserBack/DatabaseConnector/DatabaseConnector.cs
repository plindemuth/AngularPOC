namespace DatabaseUtility
{
    public class DatabaseConnector : IDatabaseConnector
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
