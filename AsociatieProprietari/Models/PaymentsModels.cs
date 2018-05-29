using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace AsociatieProprietari.Models
{
    public class PaymentsModels
    {
        [Key]
        public int Id { get; set; }
        public int FlatId { get; set; }
        public float Value { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PaymentDate { get; set; }
    }
}