﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class Paging : IPaging
    {
        public int? PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
