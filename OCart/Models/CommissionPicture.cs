using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class CommissionPicture
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CommissionId { get; set; }
        public Commission Commission { get; set; }

        public DateTime Created { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Path { get; set; }
    }
}
