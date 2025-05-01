using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")]
// [Index(nameof(Id), IsUnique = true)]
public class Photo
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }

    // [ForeignKey("AppUser")]
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
}

