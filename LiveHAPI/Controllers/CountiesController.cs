using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    [Route("api/counties")]
    public class CountiesController : Controller
    {
        [HttpGet]
        public JsonResult GetCounties()
        {
            return new JsonResult(new List<County>()
            {
                new County(){Id=47,Name="Nairobi"},
                new County(){Id=1,Name="Mombasa"}
            });
        }
        [HttpGet("{id}")]
        public JsonResult GetCounty(int id)
        {
            var list = new List<County>()
            {
                new County() {Id = 47, Name = "Nairobi"},
                new County() {Id = 1, Name = "Mombasa"}
            };

            return new JsonResult(list.FirstOrDefault(x => x.Id == id));
        }
    }
}
