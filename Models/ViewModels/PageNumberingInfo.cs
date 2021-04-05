using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10_Bowling.Models.ViewModels
{
    public class PageNumberingInfo
    {
        public int NumItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumItems { get; set; }

        // calculates number of items per page
        public int NumPages => (int)Math.Ceiling((decimal) TotalNumItems / NumItemsPerPage);
    }
}
