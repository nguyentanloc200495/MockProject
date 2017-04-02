using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MockProject.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
    public class ResetPasswordModel
    {
        [DataType(DataType.Password)]
        [Required()]
        public string Password { get; set; }

        [DataType(DataType.Password)]

        [Required()]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]

        [Required()]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
    public class AddUserPermission
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Image { get; set; }
        public int UserId { get; set; }
        public int[] SelectedPermisson { get; set; }
    }
}