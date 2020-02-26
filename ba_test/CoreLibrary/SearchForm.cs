namespace CoreLibrary
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SearchForm : IValidatableObject
    {
        public int? itemsPerPage { get; set; }

        public int? PageNum { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (itemsPerPage == null && PageNum == null)
            {
                errors.Add(new ValidationResult("Один из параметров необходимо заполнить обязательно."));
            }

            return errors;
        }
    }
}
