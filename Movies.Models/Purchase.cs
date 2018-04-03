using System;
using System.ComponentModel.DataAnnotations.Schema;
using Movies.Models.Common;

namespace Movies.Models
{
    public class Purchase : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PurchaseNumber { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }

    }
}
