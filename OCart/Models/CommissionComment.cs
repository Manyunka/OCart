using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class CommissionComment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CommissionId { get; set; }
        public Commission Commission { get; set; }

        [Required]
        public String CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        [Required]
        public String Text { get; set; }
    }
}
