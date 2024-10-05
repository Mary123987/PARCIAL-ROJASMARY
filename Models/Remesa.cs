using System.ComponentModel.DataAnnotations.Schema;

namespace PARCIAL_ROJASMARY.Models
{
    [Table("t-remesas")]
    public class Remesa
    {
    public int Id { get; set; }
    public string? Remitente { get; set; }
    public string? Destinatario { get; set; }
    public string? PaisOrigen { get; set; }
    public string? PaisDestino { get; set; }
    public decimal MontoEnviado { get; set; }
    public string? Moneda { get; set; } // "USD" o "BTC"
    public decimal TasaCambio { get; set; }
    public decimal MontoFinal { get; set; }
    public string? Estado { get; set; } 
    }
}