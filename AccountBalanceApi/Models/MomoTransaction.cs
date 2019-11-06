using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountBalanceApi.Models
{
    public class MomoTransaction
    {
        public int EntryID { get; set; }

        [Required]
        public string RefNo { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        //[DataType(DataType.PhoneNumber]
        public string MomoNumber { get; set; }

        public string MNO { get; set; }

        public string LastUpdated { get; set; }


    }
}