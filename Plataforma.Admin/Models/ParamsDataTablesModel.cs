using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plataforma.Admin.Models
{
    public class DataTablesParamsModel
    {
        [FromQuery(Name = "draw")]
        public int Draw { get; set; }

        [FromQuery(Name = "start")]
        public int Skip { get; set; }

        [FromQuery(Name = "length")]
        public int Take { get; set; }
    }
}
