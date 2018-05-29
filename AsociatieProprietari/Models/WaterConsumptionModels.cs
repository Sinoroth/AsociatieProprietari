using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsociatieProprietari.Models
{
    public class WaterConsumptionModels
    {
        [Key]
        public int Id { get; set; }
        public int FlatId { get; set; }
        public float HotCubeMeters { get; set; }
        public float ColdCubeMeters { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}")]
        public DateTime Month { get; set; }
    }
}