using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace SearchService
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] string searchTerm)
        {
            var query = DB.Find<Item>();
            query.Sort(x => x.Ascending(y => y.Name));
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query.Match(Search.Full, searchTerm).SortByTextScore();
            }
            var result = await query.ExecuteAsync();
            return result;
        }
    }
}