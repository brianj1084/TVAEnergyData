using System;
using System.ComponentModel.DataAnnotations;

namespace TVAEnergyData.Domain
{
    public class EIAPoint
    {
        [Key]
        public int Id { get; set; }
        public string SeriesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
