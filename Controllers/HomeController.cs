using Assignment10_Bowling.Models;
using Assignment10_Bowling.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10_Bowling.Controllers
{
    public class HomeController : Controller
    {
        // private context variable
        private BowlingLeagueContext _context { get; set; }

        private readonly ILogger<HomeController> _logger;

        // assign private context variable
        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext con)
        {
            _logger = logger;
            _context = con;
        }

        public IActionResult Index(long? teamid, string teamname, int pageNum = 0)
        {
            int pageSize = 5;

            return View(new IndexViewModel

            {
                Bowler = (_context.Bowlers
                .Where(t => t.TeamId == teamid || teamid == null)
                .OrderBy(b => b.BowlerFirstName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,

                    // if no team has been selected, get full count
                    // otherwise only count the number from the meal type that has been selected
                    TotalNumItems = (teamid == null ? _context.Bowlers.Count() :
                        _context.Bowlers.Where(t => t.TeamId == teamid).Count())
                },
                
                TeamName = teamname
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
