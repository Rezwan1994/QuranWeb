using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quran.Entity
{
    public class Ayath
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid AyathId { get; set; }
        public string AyathDesc { get; set; }
        public string EngDesc { get; set; }
        public string BangDesc { get; set; }
        public string AyathNo { get; set; }
        public int NoInSurah { get; set; }
        public Guid SurahId { get; set; }
    }
    [NotMapped]
    public class AyathVM : Ayath
    {
        public string AudioUrl { get; set; }
    }
}
