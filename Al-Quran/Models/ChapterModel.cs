using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Quran.Models
{
    public class ChapterModel
    {
        public string SurahName { get; set; }
        public string Ayat { get; set; }
        public string AyatBangla { get; set; }
        public string AyatEnglish { get; set; }
        public int SurahNo { get; set; }
        public int AyatNo { get; set; }
        public string AudioUrl { get; set; }
    }
}
