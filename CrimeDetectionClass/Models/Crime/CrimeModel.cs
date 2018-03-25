﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrimeDetectionClass.Models.Crime
{
    public class CrimeModel
    {
        public string Crime { get; set; }

        public string Description { get; set; }

        public string Criminal { get; set; }

        public string Location { get; set; }
    }

    public class Location
    {
        public string Trivandrum { get; set; }

        public string Kollam { get; set; }

        public string Alappuzha { get; set; }

        public string Thrissur { get; set; }
    }
}