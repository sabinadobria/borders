using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public class UserModel
    {
        [Display(Name ="Email")]
        [Required]
        //[Required(ErrorMessage ="Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Email is not valid")]
        public string email { get; set; }

        [Required]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public int userType { get; set; }


    }
}