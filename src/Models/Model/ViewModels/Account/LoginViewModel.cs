using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nhập Email")]
        public string Email { get; set; }

        [StringLength(60, MinimumLength = 8, ErrorMessage ="Mật khẩu không hợp lệ")]
        [Required(ErrorMessage ="Nhập mật khẩu")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
