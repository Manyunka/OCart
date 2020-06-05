using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class ArtistСomment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public String ArtistId { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public String CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        [Required]
        public String Text { get; set; }

    }
}
