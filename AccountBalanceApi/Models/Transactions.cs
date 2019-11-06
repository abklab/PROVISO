using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AccountBalanceApi.Models
{
    public class Transactions
    {
        public int EntryID { get; set; }
        
        public string RefNo { get; set; }
        
        public decimal Amount { get; set; }
        
        public string LastUpdated { get; set; }

        public string B_AccountNumber { get; set; }

        public string MomoNumber { get; set; }

        public string MNO { get; set; }
    }
}