using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace kpopPlaylistGenerator
{

    public class Worker : BackgroundService
    {
        static HttpClient client = new HttpClient();

        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            client.BaseAddress = new Uri("https://www.reddit.com/r/kpop/wiki/upcoming-releases/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            /*
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
            */
            //Generate URL
            DateTime date = DateTime.Today;
            String url = date.ToString("yyyy/MMMM").ToLower()+".json";

            string response = await client.GetStringAsync(url);
            _logger.LogInformation(response);
            /*if (response.IsSuccessStatusCode)
            {
                //String estoEsString = await response.Content.ReadAsAsync<String>();
                
            }*/
        }
    }
}
