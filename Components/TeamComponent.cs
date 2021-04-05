using Assignment10_Bowling.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10_Bowling.Components
{
    public class TeamComponent: ViewComponent
    {
        // create private context variable
        private BowlingLeagueContext _context { get; set; }

        // assign context variable
        public TeamComponent (BowlingLeagueContext con)
        {
            _context = con;
        }

        // filter the team
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["teamname"];

            return View(_context.Teams
                .Distinct()
                .OrderBy(t => t));
        }
    }
}
