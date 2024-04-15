using System.ComponentModel.DataAnnotations;

namespace NetAcademy.UI.Models
{
    public class BookModel //: IValidatableObject
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ISBN is unique identifier of book in common standard. It's mandatory")]
        public string ISBN { get; set; }

        [Required]
        [StringLength(100,
            ErrorMessage = "String should have from 2 to 100 symbols", 
            MinimumLength = 2)]

        public string Name { get; set; }

        //[Range(-500, 2025)]
        //[RegularExpression()]
        public int? Year { get; set; }

        [Required]
        //[DisplayFormat(DataFormatString = )]
        public decimal Price { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var errors = new List<ValidationResult>();
            
        //    //in case any issues
        //    errors.Add(new ValidationResult(""));

        //    return errors;
        //}
    }
}
