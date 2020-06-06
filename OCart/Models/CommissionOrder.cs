using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class CommissionOrder
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? CommissionId { get; set; }
        public Commission Commission { get; set; }

        [Required]
        public String CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

        public DateTime Created { get; set; }

        public StatusType Status { get; set; }

        public ICollection<CommissionOrderMessage> OrderMessages { get; set; }
    }
}
