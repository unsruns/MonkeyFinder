using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFinder.Services
{

    
    public class MonkeyService : IMonkeyService
    {
        List<Monkey> monkeys = new();
        HttpClient httpClient;
        public MonkeyService()
        {
            httpClient = new HttpClient();
        }
        public async Task<List<Monkey>> GetMonkeysAsync()
        {
            if (monkeys.Count == 0)
            {
                var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    monkeys = await response.Content.ReadFromJsonAsync<List<Monkey>>();
                }

                /*using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                monkeys = JsonSerializer.Deserialize<List<Monkey>>(json);*/
            }
            return monkeys;
        }
    }
}
