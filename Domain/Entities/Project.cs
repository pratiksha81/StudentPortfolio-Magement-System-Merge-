﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectTools { get; set; }
        public string ProjectDuration { get; set; }
        public string ProjectDescription { get; set; }

    }
}



