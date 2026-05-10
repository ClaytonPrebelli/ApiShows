using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiShows.Models;

public class Local
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Endereco { get; set; }

    [MaxLength(100)]
    public string? Cidade { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Show> Shows { get; set; } = new();
}
