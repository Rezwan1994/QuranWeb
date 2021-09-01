using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quran.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quran.Repository
{
    public class QuranDBContext:DbContext
    {
        public QuranDBContext(DbContextOptions<QuranDBContext> options):base(options)
        {

        }

        public DbSet<Surah> Surahs { get; set; }
        public DbSet<Ayath> Ayaths { get; set; }
        public DbSet<Quari> Quaris { get; set; }
        public DbSet<AudioRecitation> AudioRecitations { get; set; }

        public List<T> ExecSQL<T>(string query)
        {
            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                Database.OpenConnection();

                List<T> list = new List<T>();
                using (var result = command.ExecuteReader())
                {
                    T obj = default(T);
                    while (result.Read())
                    {
                        obj = Activator.CreateInstance<T>();
                        foreach (PropertyInfo prop in obj.GetType().GetProperties())
                        {
                            if (!object.Equals(result[prop.Name], DBNull.Value))
                            {
                                prop.SetValue(obj, result[prop.Name], null);
                            }
                        }
                        list.Add(obj);
                    }
                }
                Database.CloseConnection();
                return list;
            }
        }
    }
}
