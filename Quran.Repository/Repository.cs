using Microsoft.EntityFrameworkCore;
using Quran.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quran.Repository
{
    public class Repository
    {
        private readonly QuranDBContext _QuranDbContext;
        public Repository(QuranDBContext QuranDbContext)
        {
            _QuranDbContext = QuranDbContext;
        }

        public List<Surah> GetAllSurah()
        {
            return _QuranDbContext.Surahs.ToList();
        }
        public Surah GetSurahByName(string name)
        {
            return _QuranDbContext.Set<Surah>().Where(x=> x.EnglishName== name).FirstOrDefault();
        }
        public Surah GetSurahById(int id)
        {
            return _QuranDbContext.Set<Surah>().Find(id);
        }

        public List<Ayath> GetAllAyathBySurahId(Guid SurahId)
        {
            return _QuranDbContext.Ayaths.Where(x => x.SurahId == SurahId).ToList();
        }
        public int InsertSurah(Surah surah)
        {
            _QuranDbContext.Set<Surah>().Add(surah);
             return _QuranDbContext.SaveChanges();
        }

        public int InsertAyath(Ayath ayath)
        {
            _QuranDbContext.Set<Ayath>().Add(ayath);
            return _QuranDbContext.SaveChanges();
        }

        public int InsertAudioRecitation(AudioRecitation audio)
        {
            _QuranDbContext.Set<AudioRecitation>().Add(audio);
            return _QuranDbContext.SaveChanges();
        }
        public Ayath GetAyathByAyathNo(string AyathNo)
        {
            return _QuranDbContext.Set<Ayath>().Where(x => x.AyathNo == AyathNo).FirstOrDefault();
        }
        public int UpdateAyath(Ayath ayath)
        {
            _QuranDbContext.Entry<Ayath>(ayath).State = EntityState.Modified;
            //context.Set<TEntity>().AddOrUpdate(entity);

            return _QuranDbContext.SaveChanges();
        }

        public Quari GetQuariById(int id)
        {
            return _QuranDbContext.Set<Quari>().Find(id);
        }

        public AyathVM GetAyathbyAyathId(int SurahId,int NoInSurah)
        {
            AyathVM dsResult = new AyathVM();

            string rawQuery = @" select ayat.*,audio.AudioUrl as AudioUrl from Ayaths ayat
                                left join AudioRecitations audio on audio.AyathId = ayat.AyathId
								left join Surahs surah on surah.SurahId = ayat.SurahId
                                where surah.Id = {0} and ayat.NoInSurah = {1}
                                ";

            string sqlQuery = string.Format(rawQuery, SurahId,NoInSurah);
            //List<SalesOrderDetailVM> dsResult = context.Database.SqlQuery<SalesOrderDetailVM>(rawQuery, new object[] { }).ToList<SalesOrderDetailVM>();
            try
            {
                dsResult = _QuranDbContext.ExecSQL<AyathVM>(sqlQuery).FirstOrDefault();
               

            }
            catch (Exception ex)
            {

            }
            return dsResult;
        }
    }
}
