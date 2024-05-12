

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiasGarden.Models;

public  class ApplicationUser : IdentityUser
{
    [Required]
    [Display(Name ="Förnamn")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name ="Efternamn")]
    public string LastName { get; set; }
    [Display(Name ="Gatuadress")]
    public string? StreetAddress { get; set; }
    [Display(Name ="Ort")]
    public string? City { get; set; }
    [Display(Name ="Postnummer")]
    public string? PostalCode { get; set; }
    [NotMapped]
    public string Role {  get; set; }
 
    
}
