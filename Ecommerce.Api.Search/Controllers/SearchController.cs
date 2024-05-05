using Microsoft.AspNetCore.Mvc;
using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
namespace Ecommerce.Api.Search.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
	public readonly ISearchService _searchService;
	
	public SearchController(ISearchService searchService)
	{
		_searchService = searchService;
	}
	
	[HttpPost]
	public async Task<IActionResult> SearchAsync(SearchTerm term)
	{
		var result = await _searchService.SearchAsync(term.CustomerId);
		if (result.IsSuccess)
		{
			return Ok(result.SearchResults);
		}
		return NotFound();
	}
}
