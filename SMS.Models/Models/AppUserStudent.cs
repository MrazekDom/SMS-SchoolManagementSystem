﻿using System.ComponentModel.DataAnnotations;

namespace SMS.Models.Models {
    public class AppUserStudent {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
