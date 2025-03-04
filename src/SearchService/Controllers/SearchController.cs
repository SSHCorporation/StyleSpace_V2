using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace SearchService
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
        {
            var query = DB.PagedSearch<Item, Item>();
            if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            {
                query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
            }

            query = searchParams.OrderBy switch
            {
                "name" => query.Sort(x => x.Ascending(y => y.Name)),
                "new" => query.Sort(x => x.Descending(y => y.CreatedOn)),
                _ => query.Sort(x => x.Descending(y => y.Price))
            };

            query = searchParams.Filter switch
            {
                "cost" => query.Match(x => x.Cost > 10000),
                "price" => query.Match(x => x.Price > 1000000),
                _ => query.Match(x => x.Cost < 10000000)
            };

            query.PageNumber(searchParams.PageNumber)
                .PageSize(searchParams.PageSize);
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