using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public ActionResult GetHomeDocument()
        {
            var doc = new Dictionary<string, Link>()
            {
                {"self", new Link { Href="http://localhost:1337", Note = "Heyoo"} }

            };
            return Ok(doc);
        }
    }

    public class Link
    {
        public string Href { get; set; }
        public string Note { get; set; }
    }
}
