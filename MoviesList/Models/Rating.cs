using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesList.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int FilmId { get; set; }
        [Required]
        public int Score { get; set; }
    }
}
