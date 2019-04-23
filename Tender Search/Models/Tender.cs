using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tender_Search.Models
{
    public class Tender
    {
        [Required]
        public string TenderName { get; set; }
        [Required]
        [Key]
        public int ReferenceNumber { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime CloseDate { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
