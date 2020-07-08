using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class OrderFile
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderMessageId { get; set; }
        public OrderMessage OrderMessage { get; set; }

        public DateTime Created { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Path { get; set; }
    }
}
