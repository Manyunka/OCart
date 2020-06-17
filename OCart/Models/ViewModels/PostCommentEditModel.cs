using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models.ViewModels
{
    public class PostCommentEditModel
    {
        [Required]
        public string Text { get; set; }

    }
}
