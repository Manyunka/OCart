using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Введите e-mail")]
        [EmailAddress(ErrorMessage = "Некорректный e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
