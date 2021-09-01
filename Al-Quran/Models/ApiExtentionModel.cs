using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Quran.Models
{
    public class ApiExtentionModel
    {
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class TranslatedName
    {
        public string language_name { get; set; }
        public string name { get; set; }
    }

    public class Chapter
    {
        public int id { get; set; }
        public string revelation_place { get; set; }
        public int revelation_order { get; set; }
        public bool bismillah_pre { get; set; }
        public string name_simple { get; set; }
        public string name_complex { get; set; }
        public string name_arabic { get; set; }
        public int verses_count { get; set; }
        public List<int> pages { get; set; }
        public TranslatedName translated_name { get; set; }
    }

    public class SurahRoot
    {
        public List<Chapter> chapters { get; set; }
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Ayah
    {
        public int number { get; set; }
        public string text { get; set; }
        public string audio { get; set; }
        public List<string> audioSecondary { get; set; }
        public int numberInSurah { get; set; }

        public int page { get; set; }
  
    }

    public class Surah
    {
        public int number { get; set; }
        public string name { get; set; }
        public string englishName { get; set; }
        public string englishNameTranslation { get; set; }
        public string revelationType { get; set; }
        public List<Ayah> ayahs { get; set; }
    }

    public class Edition
    {
        public string identifier { get; set; }
        public string language { get; set; }
        public string name { get; set; }
        public string englishName { get; set; }
        public string format { get; set; }
        public string type { get; set; }
    }
    public class Vers
    {
        public int id { get; set; }
        public string verse_key { get; set; }
        public string text_indopak { get; set; }
    }

    public class BanglaTextModel
    {
        public List<Vers> verses { get; set; }
    }
   
    public class Data
    {
        public List<Surah> surahs { get; set; }
        public Edition edition { get; set; }
    }

    public class AyathRoot
    {
        public int code { get; set; }
        public string status { get; set; }
        public Data data { get; set; }
    }




}
