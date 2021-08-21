using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCakeShopeBoutiqe.Models
{
    public class Client
    {
        [Required]
        [Key]
        public int ClientId { get; set; }


        [Required(ErrorMessage = "Enter your full name")]
        [StringLength(maximumLength: 20)]
        [Display(Name = "Full Name")]
        public string ClientName { get; set; }



        [Required(ErrorMessage = "Enter youre password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", ErrorMessage = "Enter stronger password")]
        [DataType(DataType.Password)]
        [StringLength(20)]

        public string Password { get; set; }



        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", ErrorMessage = "Enter stronger password")]
        [Compare("Password", ErrorMessage = "you entered wrong Password ")]
        [StringLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirm")]

        public string PasswordConfirm { get; set; }


        [Required(ErrorMessage = "Enter your email adrress")]
        [StringLength(20)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]

        public string EmailAddress { get; set; }


        //public List<Purchase> Purchases { get; set; } = null; //List of history orders

    }
}
