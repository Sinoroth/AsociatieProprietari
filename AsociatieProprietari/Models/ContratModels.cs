using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsociatieProprietari.Models
{
    public class ContratModels
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string Resource { get; set; }
        public float Value { get; set; }
    }
}