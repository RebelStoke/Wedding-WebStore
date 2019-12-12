using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using WeddingApp.Core.ApplicationService;

namespace WeddingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateController : ControllerBase
    {
        private readonly IDateService _dateService;

        public DateController(IDateService dateService)
        {
            _dateService = dateService;
        }

        // GET: api/Date
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dateService.GetAllDatesAssigned());
        }

        // POST: api/Date
        [HttpPost]
        public IActionResult Post([FromBody] JObject data)
        {
            return Ok(_dateService.GetAllDatesForMonth(Convert.ToInt32(data["year"].ToString()), Convert.ToInt32(data["month"].ToString())));
        }
    }
}