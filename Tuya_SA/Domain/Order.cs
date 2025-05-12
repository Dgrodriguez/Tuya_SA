using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Tuya_SA.Domain;


public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    public Customer? Customer { get; set; }

    public string Status { get; set; } = "Pending";
}