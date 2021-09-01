using Al_Quran.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Quran.Entity;
using Quran.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Quran.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository _repo;

        public HomeController(Repository repo)
        {
            _repo = repo;
        }
        List<ChapterModel> chapterList = new List<ChapterModel>();
        public IActionResult Index()
        {

     
           
            return View();
        }
        public IActionResult PlayAudio(int surahId,int noInSurah,bool? IsPrevious)
        {

            Quran.Entity.Surah surah = _repo.GetSurahById(surahId);
            List<Ayath> AyathList = _repo.GetAllAyathBySurahId(surah.SurahId);
            ViewBag.SurahDropdown = _repo.GetAllSurah().Select(x =>
               new SelectListItem()
               {
                   Text = x.Id + "." + x.EnglishName.ToString() + "-" + x.SurahName,
                   Value = x.Id.ToString()
               }).ToList();
            ViewBag.AyathDropdown = AyathList.Select(x =>
               new SelectListItem()
               {
                   Text =  "Ayat " + x.NoInSurah,
                   Value = x.NoInSurah.ToString()
               }).ToList();
            if(surahId >1 && noInSurah == 1)
            {
                Quran.Entity.Surah surahPrev = _repo.GetSurahById(surahId-1);
                ViewBag.TotalAyath = _repo.GetAllAyathBySurahId(surahPrev.SurahId).Count();
            }
            else
            {
                ViewBag.TotalAyath = _repo.GetAllAyathBySurahId(surah.SurahId).Count();
            }
           
            ChapterModel model = new ChapterModel();
            AyathVM ayat = _repo.GetAyathbyAyathId(surah.Id,noInSurah);
            model.AudioUrl = ayat.AudioUrl;
            model.Ayat = ayat.AyathDesc;
            model.AyatBangla = ayat.BangDesc;
            model.AyatEnglish = ayat.EngDesc;
            model.SurahNo = surah.Id;
            model.AyatNo = ayat.NoInSurah;
            return View(model);
        }

        public JsonResult GetLastAyath([FromBody] int SurahId)
        {
            Quran.Entity.Surah surah = _repo.GetSurahById(SurahId);
            int Ayath = _repo.GetAllAyathBySurahId(surah.SurahId).Count();
            return Json(new { Ayath = Ayath });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
