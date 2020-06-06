using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class CommissionOrderFile
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CommissionOrderMessageId { get; set; }
        public CommissionOrderMessage CommissionOrderMessage { get; set; }

        public DateTime Created { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Path { get; set; }
    }
}
