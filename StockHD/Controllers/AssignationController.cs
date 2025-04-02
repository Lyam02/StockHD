using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockLibrary.Models;
using StockLibrary;

namespace StockHD.Controllers
{
    public class AssignationController : Controller
    {
        private readonly ILogger<AssignationController> _logger;
        private readonly StockDbContext _context;

        public AssignationController(ILogger<AssignationController> logger, StockDbContext context)
        {
            _logger = logger;
            _context = context;
        }


    }

}
