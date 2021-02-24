
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankAccounts.Models
{
  public class Account
  {
    public TransacM userTransaction { get; set; }
    public List<TransacM> allTransaction { get; set; }
  }
}