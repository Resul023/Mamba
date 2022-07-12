using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mamba.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Key { get; set; }
        [Required]
        [MaxLength(30)]
        public string Value { get; set; }

    }
}
