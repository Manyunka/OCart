using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid DialogId { get; set; }
        public Dialog Dialog { get; set; }

        [Required]
        public String CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        [Required]
        public String Text { get; set; }

        public ICollection<MessageFile> MessageFiles { get; set; }
    }
}
