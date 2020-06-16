using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OCart.Models.ViewModels
{
    public class PostCreateModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public IList<IFormFile> Pictures { get; set; }
    }
}
