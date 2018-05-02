using System;
using System.ComponentModel.DataAnnotations;


namespace MvcMovie.Models
{
    public class Movie
    {

        public int ID { get; set; }

        [StringLength(60, ErrorMessage = "Please enter a title between 3 and 60 characters long.", MinimumLength = 3)]
        [Required(ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter a date.")]
        [DisplayFormat(DataFormatString = "{0:MMM. dd, yyyy}")]
        public DateTime? ReleaseDate { get; set; }

        [Range(0, 30, ErrorMessage = "Please enter a valid price below $29.99.")]
        //[RegularExpression("([0-9].+)", ErrorMessage = "You can only enter numbers in the 'Price' field.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a price.")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Please enter a valid genre.")]
        [Required(ErrorMessage = "Please enter a genre.")]
        [StringLength(30)]
        public string Genre { get; set; }

        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s-]*$", ErrorMessage = "Please select a rating.")]
        [StringLength(5, ErrorMessage = "Please enter a valid movie rating.")]
        [Required]
        public string Rating { get; set; }

    }
}