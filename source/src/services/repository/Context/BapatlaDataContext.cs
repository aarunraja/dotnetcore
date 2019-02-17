namespace Bapatla.CMS.Repository
{
    using Bapatla.CMS.Core.Repository;
    using Bapatla.CMS.Domain;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    public class BapatlaDataContext 
    {
        public IMongoDatabase Database { get; }

        public BapatlaDataContext(IOptions<DBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                Database = client.GetDatabase(settings.Value.Database);
        }

       
    }
}