using System;

namespace DatabaseUtility
{
    public interface IDatabaseConnector
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
