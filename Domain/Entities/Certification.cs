﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Certification
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReceivedDate { get; set; }

        // Foreign key property
        public int StudentId { get; set; } // Ensure this exists only once

        // Navigation property
        public Student Student { get; set; }
    }

}
