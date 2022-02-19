using E5irProjet.Models;
using E5irProjet.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E5irProjet.Controllers
{
     [ApiController]
    [Route("[controller]")]
   
    public class RadioController : Controller
    {
       // DataParameters parms = new DataParameters();

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Get([FromBody] DataParameters summary)
        {
            DataParameters parms = new DataParameters();
            var csvParserService = new CsvParserService();
             csvParserService.ParserWS(summary);
            return new JsonResult("Added Successfully ** ");
        }

    }
}
