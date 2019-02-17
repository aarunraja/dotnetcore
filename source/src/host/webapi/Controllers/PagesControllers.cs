namespace Bapatla.CMS.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bapatla.CMS.Contract;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/Pages")]
    [ApiController]
    public class PagesControllers : ControllerBase
    {
        private readonly IPageService _pageService;

        public PagesControllers(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> Get()
        {
           var lstPage = await _pageService.GetAllPages();
            return Ok(lstPage);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Page>> Get(string id)
        {
            var page = await _pageService.GetPage(id);
            return Ok(page);
        }

        
        [HttpPost]
        public async Task<ActionResult<Page>> Post([FromBody] Page page)
        {
             await _pageService.Create(page);
            return CreatedAtAction("", page);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Page page)
        {
            var rtn = await _pageService.Update(page);
            return Ok();
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var rtn = await _pageService.Delete(id);
            return Ok();
        }
    }
}
