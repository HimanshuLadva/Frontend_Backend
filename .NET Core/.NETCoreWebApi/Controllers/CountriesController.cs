using ConsoleToWebApi.Models;
using ConsoleToWebApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [BindProperties(SupportsGet = true)]
    public class CountriesController : ControllerBase
    {
        // for post
        //[BindProperty]
        //for get
        //[BindProperty(SupportsGet = true)]
        // public CountryModel Country {get; set;}

        // for simple data
        //[HttpGet("{name}/{population}/{area}")]
        //for complex data

        /* [HttpPost("")]
        public IActionResult AddCountry(CountryModel country)
        {
            return Ok($"Name = {country.Name}, Population = {country.Population}, Area = {country.Area}");
        } */

        [HttpPost("{id}")]
        public IActionResult AddCountry([FromRoute] int id, [FromHeader]CountryModel countryModel)
        {
            //  return Ok($"Id =  {id}, Name = {name}");
            return Ok($"Name = {countryModel.Name}, Population = {countryModel.Population}, Area = {countryModel.Area}");
        }

        [HttpGet("search")]
        public IActionResult SearchCountries([ModelBinder(typeof(CustomeModelBinder))]string[] countries) 
        {
            return Ok(countries);
        }

        [HttpGet("name")]
        public IActionResult GetName([FromServices] IProductRespository _productRespository1)
        {
            var name = _productRespository1.GetName();
            return Ok(name);
        }
    }
}