using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuestbookSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string AuthorEmail { get; set; }
        [DisplayName("用户名")]
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        [DisplayName("密码")]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
        public SystemRole SRole { get; set; }

        public virtual ICollection<Guestbook> Guestbooks { get; set; }
    }
    public enum SystemRole { 普通用户,管理员}
}