using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AslMonitor.DAL.Models
{
    public class LoginToken
    {
        [Key]
        public int LoginTokenID { get; set; }
        [Required]
        public string Token { get; set; } = default!;
    }
}
