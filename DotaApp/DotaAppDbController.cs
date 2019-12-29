
using DotaApp.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotaApp
{
    public class DotaAppDbController
    {
        private readonly SQLiteAsyncConnection _database;
        //private readonly string _url = "https://api.opendota.com/api/heroes";
        //private HttpClient _client;
        public DotaAppDbController(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
           /* _database.CreateTableAsync<Hero>().Wait();

            _client = new HttpClient();
            try
            {
                
                var resp = _client.GetAsync(_url);

                var result = JsonConvert.DeserializeObject<List<Hero>>(resp.Result.Content.ReadAsStringAsync().Result);
                Debug.WriteLine("Here");
                Debug.WriteLine(result.Count);
                foreach (Hero hero in result)
                {
                    Debug.WriteLine(hero.id);
                    Debug.WriteLine(hero.localized_name);
                    SaveHeroAsync(hero);
                }

            }
            catch (Exception e)
            {
                throw e;
            }*/
            
        }

        public  Task<List<Hero>> GetHeroListAsync()
        {
            if (_database != null)
            {
                //Debug.WriteLine("database is not null");
                return _database.Table<Hero>().ToListAsync();
            }
            //Debug.WriteLine("Problem here");
            return null;
        }

        public Task<Hero> GetHeroAsync(int id) 
        {
            return _database.Table<Hero>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveHeroAsync(Hero hero)
        {
            return _database.InsertAsync(hero);
        }

        public Task<int> DropDatabaseAsync()
        {
            return _database.DropTableAsync<Hero>();
        }

    }

    
}
