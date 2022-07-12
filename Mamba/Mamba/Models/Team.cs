using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mamba.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Fullname { get; set; }
        [Required]
        [MaxLength(15)]
        public string Position { get; set; }
        [Required]
        [MaxLength(150)]
        public string TwitUrl { get; set; }
        [Required]
        [MaxLength(150)]
        public string FaceUrl { get; set; }
        [Required]
        [MaxLength(150)]
        public string InstaUrl { get; set; }
        [Required]
        [MaxLength(150)]
        public string LikdnUrl { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }


    }
}
