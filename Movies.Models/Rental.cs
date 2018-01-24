using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Movies.Models.Common;

namespace Movies.Models
{
    public class Rental : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderGuid { get; set; }

        public string CustomerId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime RentalDateTime { get; set; }
        public int RentalDurationHours { get; set; }
        public DateTime RentalExpireDate { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
    }

    public enum OrderStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10,

        /// <summary>
        /// Processing
        /// </summary>
        Processing = 20,

        /// <summary>
        /// Complete
        /// </summary>
        Complete = 30,

        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled = 40
    }

    /// <summary>
    /// Represents a payment status enumeration
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10,

        /// <summary>
        /// Authorized
        /// </summary>
        Authorized = 20,

        /// <summary>
        /// Paid
        /// </summary>
        Paid = 30,

        /// <summary>
        /// Partially Refunded
        /// </summary>
        PartiallyRefunded = 35,

        /// <summary>
        /// Refunded
        /// </summary>
        Refunded = 40,

        /// <summary>
        /// Voided
        /// </summary>
        Voided = 50,
    }
}