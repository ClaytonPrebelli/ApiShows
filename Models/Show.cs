using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiShows.Models;

public class Show
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int ContratanteId { get; set; }

    [Required]
    public int LocalId { get; set; }

    public Contratante? Contratante { get; set; }

    public Local? Local { get; set; }

    [Required]
    public DateTime Data { get; set; }

    [Required]
    [MaxLength(10)]
    public string Hora { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Duracao { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal ValorCobrado { get; set; }

    public bool Pago { get; set; }

    public DateTime? DataPagamento { get; set; }

    [MaxLength(50)]
    public string FormaPagamento { get; set; } = string.Empty;

    public List<string> EstilosSolicitados { get; set; } = new();

    public bool NecessitaNotaFiscal { get; set; }

    public bool NotaEmitida { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
