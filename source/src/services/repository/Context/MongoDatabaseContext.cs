namespace MLC.General.Repository
{
    using MLC.Core.Repository;
    using MLC.General.Domain;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    public class MongoDatabaseContext 
    {
        public IMongoDatabase Database { get; }

        public MongoDatabaseContext(IOptions<DBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                Database = client.GetDatabase(settings.Value.Database);
        }
    }
}