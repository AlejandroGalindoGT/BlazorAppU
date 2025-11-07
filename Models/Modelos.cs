using System.ComponentModel.DataAnnotations;

namespace BlazorAppU.Models
{
    public class User
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        public Address address { get; set; } = new();

        [Phone]
        public string phone { get; set; }

        [Url]
        public string website { get; set; }

        public Company company { get; set; } = new();
    }

    public class Address
    {
        [Required]
        public string street { get; set; }

        public string suite { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Código postal inválido.")]
        public string zipcode { get; set; }

        public Geo geo { get; set; } = new();
    }

    public class Geo
    {
        [Required]
        public string lat { get; set; }

        [Required]
        public string lng { get; set; }
    }

    public class Company
    {
        [Required]
        public string name { get; set; }

        public string catchPhrase { get; set; }

        public string bs { get; set; }
    }
}
