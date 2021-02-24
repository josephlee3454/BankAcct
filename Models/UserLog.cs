 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankAccounts.Models
{
    public class UserLog
    {
        [Key]
        public int UserLogID {get;set;}
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string LogEm {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string LogPass {get;set;}
    }
}

