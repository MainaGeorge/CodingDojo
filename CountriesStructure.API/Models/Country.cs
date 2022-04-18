using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountriesStructure.API.Models
#nullable disable
{
    public class Country
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Code { get; set; }

        [ForeignKey(nameof(TopNeighbour))]
        public string TopNeighbourCode { get; set; }

        public TopNeighbour TopNeighbour { get; set; }


    }
}
