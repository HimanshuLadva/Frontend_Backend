using ConsoleToWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Route("")]
        public List<EmployeeModel> GetEmployees()
        {
            return new List<EmployeeModel> { 
               new EmployeeModel() { Id = 1, Name ="Himanshu"},
               new EmployeeModel() { Id = 2, Name ="Darshit"},
               new EmployeeModel() { Id = 3, Name ="Shivam"},
            };
        }

        [Route("{id}")]
        public IActionResult GetEmployees(int id)
        {
            if(id== 0)
            {
               return NotFound();
            }
            return Ok(new List<EmployeeModel> {
               new EmployeeModel() { Id = 1, Name ="Himanshu"},
               new EmployeeModel() { Id = 2, Name ="Darshit"},
            }); 
        }

        [Route("{id}/basic")]
        public ActionResult<EmployeeModel> GetEmployeesBasicDetail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            return Ok(new List<EmployeeModel> {
               new EmployeeModel() { Id = 1, Name ="Himanshu"},
            });
        }
    }
}
