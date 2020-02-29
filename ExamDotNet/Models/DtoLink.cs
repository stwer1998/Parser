using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamDotNet.Models
{
    public class DtoLink
    {
        [Required]
        public Uri Link { get; set; }
        
        [Required]
        [Range(0,100)]
        public int Depth { get; set; }

        [Required]
        [Range(0, 100)]
        public int Limit { get; set; }
    }
}
