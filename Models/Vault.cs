using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace keepr.Models
{
    public class Vault
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public string UserId { get; set; }


    }

}