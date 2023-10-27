using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardsApi.Models
{
    public partial class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(85, ErrorMessage = "First name cannot exceed 85 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(85, ErrorMessage = "Last name cannot exceed 85 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Invalid phone number. It must be 12 digits.")]
        public long? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Card is required.")]
        public short? Card { get; set; }

        public virtual Card? CardNavigation { get; set; }
    }
}
