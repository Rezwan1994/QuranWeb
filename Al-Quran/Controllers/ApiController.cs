using Al_Quran.Models;
using Microsoft.AspNetCore.Mvc;
using Quran.Entity;
using Quran.Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Quran.Controllers
{
    public class ApiController : Controller
    {
        private readonly Repository _repo;

        public ApiController(Repository repo)
        {
            _repo = repo;
        }
        public IActionResult InsertSurah()
        {
            return View();
        }
        public void AddAllSurahNameToDb()
        { 
            var client = new RestClient("https://api.quran.com/api/v4/chapters?language=bn");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
        

            SurahRoot root  = Newtonsoft.Json.JsonConvert.DeserializeObject<SurahRoot>(response.Content);
           
            if(root != null)
            {
                foreach(var item in root.chapters)
                {
                    Quran.Entity.Surah surah = new Quran.Entity.Surah();
                    surah.SurahName = item.name_arabic;
                    surah.BanglaName = item.translated_name.name;
                    surah.EnglishName = item.name_simple;
                    surah.RevelationPlace = item.revelation_place;
                    surah.SurahId = Guid.NewGuid();
                    _repo.InsertSurah(surah);
                }
            }
        }

        public void AddAllAyathToDb()
        {
            var client = new RestClient("http://api.alquran.cloud/v1/quran/ar.muyassar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);


            AyathRoot root = Newtonsoft.Json.JsonConvert.DeserializeObject<AyathRoot>(response.Content);

            if (root != null)
            {
                foreach (var item in root.data.surahs) 
                {
                   Quran.Entity.Surah surah = _repo.GetSurahById(item.number);
                  
                    foreach(var ayathitem in item.ayahs)
                    { 
                        Quran.Entity.Ayath ayath = new Quran.Entity.Ayath();
                        if (surah != null)
                        {
                            ayath.SurahId = surah.SurahId;
                        }
                        ayath.AyathDesc = ayathitem.text;
                        ayath.AyathNo  = ayathitem.number.ToString();
                        ayath.NoInSurah = ayathitem.numberInSurah;
                        ayath.AyathId = Guid.NewGuid();
                        _repo.InsertAyath(ayath);
                    }
                
                  
                }
            }
        }

        public void UpdateAyathForEnText()
        {
            var client = new RestClient("http://api.alquran.cloud/v1/quran/en.ahmedali");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);


            AyathRoot root = Newtonsoft.Json.JsonConvert.DeserializeObject<AyathRoot>(response.Content);

            if (root != null)
            {
                foreach (var item in root.data.surahs)
                {
                 
                    foreach (var ayathitem in item.ayahs)
                    {
                        Quran.Entity.Ayath ayath = new Quran.Entity.Ayath();
                      
                        ayath = _repo.GetAyathByAyathNo(ayathitem.number.ToString());
                        if(ayath != null)
                        {
                            ayath.EngDesc = ayathitem.text;
                            _repo.UpdateAyath(ayath);
                        }
                    }

                  
                }
            }
        }     
        public void UpdateAyathForBnText()
        {
            var client = new RestClient("http://api.alquran.cloud/v1/quran/bn.bengali");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);


            AyathRoot root = Newtonsoft.Json.JsonConvert.DeserializeObject<AyathRoot>(response.Content);

            if (root != null)
            {
                foreach (var item in root.data.surahs)
                {

                    foreach (var ayathitem in item.ayahs)
                    {
                        Quran.Entity.Ayath ayath = new Quran.Entity.Ayath();

                        ayath = _repo.GetAyathByAyathNo(ayathitem.number.ToString());
                        if (ayath != null)
                        {
                            ayath.BangDesc = ayathitem.text;
                            _repo.UpdateAyath(ayath);
                        }
                    }


                }
            }
        }

        public void AddAllAudioRecitaionToDb()
        {
            var client = new RestClient("http://api.alquran.cloud/v1/quran/ar.alafasy");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);


            AyathRoot root = Newtonsoft.Json.JsonConvert.DeserializeObject<AyathRoot>(response.Content);
            Quari Quari = _repo.GetQuariById(1);
            if (root != null)
            {
                foreach (var item in root.data.surahs)
                {
                    Quran.Entity.Surah surah = _repo.GetSurahById(item.number);

                    foreach (var ayathitem in item.ayahs)
                    {
                        Quran.Entity.Ayath ayath = new Quran.Entity.Ayath();
                        AudioRecitation audio = new AudioRecitation();
                        ayath = _repo.GetAyathByAyathNo(ayathitem.number.ToString());
                        audio.AudioUrl = ayathitem.audio;
                        audio.AyathId = ayath.AyathId;
                        audio.QuariId = Quari.QuariId;
                       
                        _repo.InsertAudioRecitation(audio);
                    }


                }
            }
        }

        public void UpdateAyatInArbi()
        {
            var client = new RestClient("https://api.quran.com/api/v4/quran/verses/indopak");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);


            BanglaTextModel root = Newtonsoft.Json.JsonConvert.DeserializeObject<BanglaTextModel>(response.Content);
            Quari Quari = _repo.GetQuariById(1);
            if (root != null)
            {
                foreach (var item in root.verses)
                { 
                    Quran.Entity.Ayath ayath = new Quran.Entity.Ayath();
                   
                    ayath = _repo.GetAyathByAyathNo(item.id.ToString());
                    ayath.AyathDesc = item.text_indopak;
                    _repo.UpdateAyath(ayath);
                    
                }
            }
        }
    }
}
