using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class Dialog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public String CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
    }
}
