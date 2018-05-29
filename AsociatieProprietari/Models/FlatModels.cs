using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsociatieProprietari.Models
{
    public class FlatModels
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string ApartmentNumber { get; set; }
        public string FullAddress { get; set; }
        public int NumberOfPersons { get; set; }
        public int NumberOfRooms { get; set; }
    }
}