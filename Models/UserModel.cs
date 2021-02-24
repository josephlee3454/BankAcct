using System.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bankAccounts.Models;
using Microsoft.EntityFrameworkCore;
namespace bankAccounts.Models
{
    public class UserModel
    {
        [Key]
        public int UserId{get;set;}

        [Required]
        [MinLength(2)]
        [Display(Name = "First Name")]
        public string first_name{get;set;}


        [Required]
        [MinLength(2)]
        [Display(Name = " Last Name")]
        public string last_name{get;set;}

        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Compare("ConfrimPass")]
        [MinLength(8)]
        public string Pass{get;set;}

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [Display(Name= "confirm Password")]
        public String ConfirmPass {get;set;}


        public List<TransacM> TransacM {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;




    }



}