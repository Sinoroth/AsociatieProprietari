using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsociatieProprietari.Models
{
    public class SituationViewModels
    {
        public IEnumerable<InvoiceModels> Invoices { get; set; }
        public IEnumerable<PaymentsModels> Payments { get; set; }
        public float TotalDebt { get; set; }
        public float TotalPayments { get; set; }
        public float TotalRemaining { get; set; }
    }
}