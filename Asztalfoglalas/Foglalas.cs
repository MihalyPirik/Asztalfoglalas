using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asztalfoglalas
{
    public class Foglalas
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nev { get; set; }

        [StringLength(30)]
        public string Telefonszam { get; set; }

        [Required]
        public DateTime Datum { get; set; }

        [Required]
        public int Letszam { get; set; }

        [Required]
        public Asztal Asztal { get; set; }

        [Required]
        public int AsztalID { get; set; }
    }
}
