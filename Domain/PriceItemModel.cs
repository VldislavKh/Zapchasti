﻿using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PriceItemModel
    {
        public string Vendor { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string Count { get; set; }
    }
}
