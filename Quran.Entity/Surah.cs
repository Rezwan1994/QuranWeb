using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quran.Entity
{
    public class Surah
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid SurahId { get; set; }
        public string SurahName { get; set; }
        public string RevelationPlace { get; set; }
        public string BanglaName { get; set; }
        public string EnglishName { get; set; }
    }
}
