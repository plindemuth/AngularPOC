using ApiUtilities;
using ExcelParser.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExcelParserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelParserController : ControllerBase
    {
        private readonly IExcelParserService _service;

        public ExcelParserController(IExcelParserService excelParserService)
        {
            _service = excelParserService;
        }

        // GET: api/<ExcelParserController>
        [HttpGet]
        [Route("get")]
        public IActionResult GetEntries()
        {
            try
            {
                var entries = _service.GetEntries();
                return new OkObjectResult(entries);
            }
            catch
            {
                return Problem();
            }
        }

        // POST api/<ExcelParserController>
        [HttpPost]
        [Route("upload")]
        public IActionResult UploadSpreadsheet()
        {
            try
            {
                var docInfo = MultiPartRequestUtility.ProcessRequest(Request);
                _service.UploadNew(docInfo);
                return new OkObjectResult(2);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost]
        [Route("test-data")]
        public IActionResult InsertTestData()
        {
            try
            {
                _service.InsertTestData();
                return new NoContentResult();
            }
            catch
            {
                return Problem();
            }
            
        }
    }
}
