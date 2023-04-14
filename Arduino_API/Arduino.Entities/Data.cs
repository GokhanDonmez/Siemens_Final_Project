using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arduino.Entities
{
    public class Data
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Fuel { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
}