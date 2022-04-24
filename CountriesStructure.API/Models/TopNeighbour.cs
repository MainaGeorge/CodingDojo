using System.ComponentModel.DataAnnotations;

namespace CountriesStructure.API.Models;

#nullable disable
public class TopNeighbour 
{
    [Key]
    [StringLength(maximumLength:3, MinimumLength = 3)]
    public string Code { get; init; }
    public ICollection<Country> Countries { get; set; } 
}