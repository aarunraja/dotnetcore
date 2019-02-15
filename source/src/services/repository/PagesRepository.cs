namespace Bapatla.CMS.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Bapatla.CMS.Domain;
    
    public class PagesRepository : IPagesRepository
    {
        private readonly IPageContext _context;

        public PagesRepository(IPageContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Page>> GetAllPages()
        {
            return await _context
                            .Pages
                            .Find(_ => true)
                            .ToListAsync();
        }

        public Task<Page> GetPage(string path)
        {
            FilterDefinition<Page> filter = Builders<Page>.Filter.Eq(m => m.Slug, path);

            return _context
                    .Pages
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
       
        public async Task Create(Page Page)
        {
            await _context.Pages.InsertOneAsync(Page);
        }

        public async Task<bool> Update(Page Page)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Pages
                        .ReplaceOneAsync(
                            filter: g => g.Id == Page.Id,
                            replacement: Page);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string path)
        {
            FilterDefinition<Page> filter = Builders<Page>.Filter.Eq(m => m.Slug, path);

            DeleteResult deleteResult = await _context
                                                .Pages
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
