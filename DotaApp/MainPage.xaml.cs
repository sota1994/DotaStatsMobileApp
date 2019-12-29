using DotaApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DotaApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

   
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //private string _apiKey = "a6af7ec1-146c-4741-bb53-a9dae802d395";
        private long _steamId = 94731961;
        private string _url = "https://api.opendota.com/api/players/";
        private HttpClient _client;
        private Dictionary<string, int> Appear = new Dictionary<string, int>();
        
        public MainPage()
        {
            InitializeComponent();
            Update();
           


        }

        public async void Update()
        {
            _client = new HttpClient();
            try
            {
                var url = _url + _steamId.ToString() + "/recentMatches";
                //Debug.WriteLine("url");
                //Debug.WriteLine(url);
                var resp = _client.GetAsync(url);
                //var result = await resp.Result.Content.ReadAsStringAsync();
                //Debug.WriteLine("Here");
                //Debug.WriteLine(result);

                var result = JsonConvert.DeserializeObject<List<RecentMatch>>(resp.Result.Content.ReadAsStringAsync().Result);
                Debug.WriteLine("Here");
                Debug.WriteLine(result.Count);

                var base_url = "https://api.opendota.com/api/matches/";
                int total_gpm = 0;
                int total_xpm = 0;
                long avg_networth = 0;
                long avg_total_xp = 0;
                List<int> neutral_farm = new List<int>();
                int total_lane_farm = 0;
                foreach (RecentMatch rm in result)
                {
                    var target_url = base_url + rm.match_id;
                    Debug.WriteLine(target_url);
                    resp = _client.GetAsync(target_url);
                    //var result1 = await resp.Result.Content.ReadAsStringAsync();
                    //Debug.WriteLine(result1);
                    var match = JsonConvert.DeserializeObject<Match>(resp.Result.Content.ReadAsStringAsync().Result);
                    long radiant_networth = 0;
                    long dire_networth = 0;
                    long radiant_total_xp = 0;
                    long dire_total_xp = 0;
                    

                    foreach (Player p in match.Players)
                    {
                        if (p.SteamId != _steamId)
                        {
                            var hero = await App.Database.GetHeroAsync(p.HeroId);
                            //Debug.WriteLine(hero.Id);
                            //Debug.WriteLine(hero.localized_name);
                            if (Appear.ContainsKey(hero.localized_name))
                            {
                                Appear[hero.localized_name]++;
                            }
                            else
                            {
                                Appear.Add(hero.localized_name, 1);
                            }

                        }
                        else
                        {
                            total_gpm += p.GPM;
                            total_xpm += p.XPM;
                            neutral_farm.Add(p.NeutralFarm);
                            total_lane_farm += p.LaneFarm;
                        }
                           
                        if (p.IsRadiant) {
                            radiant_networth += p.Networth;
                            radiant_total_xp += p.TotalXP;
                        }
                        else
                        {
                            dire_networth += p.Networth;
                            dire_total_xp += p.TotalXP;
                        }
                    }

                    avg_networth += Math.Abs(radiant_networth - dire_networth) ;
                    avg_total_xp += Math.Abs(radiant_total_xp - dire_total_xp) ;

                }
                var avg_gpm = total_gpm / result.Count;
                var avg_xpm = total_xpm / result.Count;
               
                var ordered = Appear.OrderByDescending(x => x.Value);
                /*foreach (KeyValuePair<string, int> kvp in ordered)
                {
                    Debug.WriteLine("Hero = {0}, Count = {1}", kvp.Key, kvp.Value);
                }*/
                Debug.WriteLine("test neutral");
                var total_netral_farm = 0;
                foreach (int item in neutral_farm)
                {
                    total_netral_farm += item;
                    //Debug.WriteLine(item);
                }
                var avg_neutral_farm = total_netral_farm / result.Count();
                lvHeroes.ItemsSource = ordered;
                lblGPM.Text = avg_gpm.ToString();
                lblXPM.Text = avg_xpm.ToString();
                lblNeutralCS.Text = avg_neutral_farm.ToString();
                lblLaneCS.Text = (total_lane_farm / result.Count).ToString();
                lblGoldDiference.Text = (avg_networth/result.Count).ToString();
                lblXPDiference.Text = (avg_total_xp/result.Count).ToString();


            }
            catch (Exception e)
            {
                throw e;
            }
            //await App.Database.GetHeroListAsync();
            //lvHeroes.ItemsSource = await App.Database.GetHeroListAsync();
            //await App.Database.DropDatabaseAsync();
        }

        
    }
}
