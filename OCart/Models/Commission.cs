using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCart.Models
{
    public class Commission
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public String CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        [Required]
        [MaxLength(200)]
        public String Title { get; set; }
        [Required]
        public String Description { get; set; }

        [Column(TypeName = "Money")]
        public Decimal Price { get; set; }

        public ICollection<CommissionComment> CommissionComments { get; set; }
        public ICollection<CommissionPicture> CommissionPictures { get; set; }
        public ICollection<CommissionOrder> CommissionOrders { get; set; }
    }
}
