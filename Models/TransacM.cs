using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bankAccounts.Models;
using Microsoft.EntityFrameworkCore;



namespace bankAccounts.Models
{

    public class TransacM
    {
        [Key]
        public int TransId {get;set;}
        [Required]
        [Display(Name="Add/Pull")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:0.###}")]
        public decimal TotalAmt { get; set; }
        public int UserId {get;set;}
        public UserModel AcctOwner { get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

}