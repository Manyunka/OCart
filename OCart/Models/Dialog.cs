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
        public String UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public String InterlocutorId { get; set; }
        public ApplicationUser Interlocutor { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
