using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace SearchService
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            var query = DB.PagedSearch<Item>();
            query.Sort(x => x.Ascending(y => y.Name));
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query.Match(Search.Full, searchTerm).SortByTextScore();
            }
            query.PageNumber(pageNumber).PageSize(pageSize);
            var result = await query.ExecuteAsync();
            return Ok(new
            {
                totalCount = result.TotalCount,
                pageCount = result.PageCount,
                results = result.Results
            });
        }
    }
}