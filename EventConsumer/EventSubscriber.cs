using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using static System.Console;

namespace EventConsumer
{
    public class EventSubscriber
    {
        private readonly string shoppingCartHost;
        public DateTimeOffset lastPollTime;

        public EventSubscriber(string shoppingCartHost)
        {
            this.lastPollTime = DateTimeOffset.MinValue;
            this.shoppingCartHost = shoppingCartHost;
        }

        public async Task SubscriptionCycleCallback()
        {
            var response = await ReadEvents().ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HandleEvents(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                this.lastPollTime = DateTimeOffset.Now;
            }
        }

        public async Task<HttpResponseMessage> ReadEvents()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"http://{this.shoppingCartHost}");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var to = DateTimeOffset.Now;

                Console.WriteLine($"Fetching event that occurred from '{lastPollTime}' to '{to}'...");

                var requestUri = $"/shoppingcart/events?from={this.lastPollTime}&to={to}";
                var response = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
                return response;
            }
        }

        public void HandleEvents(string content)
        {
            var events = JsonConvert.DeserializeObject<List<Event>>(content);

            if(events.Count == 0)
            {
                Console.WriteLine($"No new events...");
            }

            foreach (var ev in events)
            {
                Console.WriteLine($"Event of Type '{ev.Name}' occured at: {ev.OccuredAt}");
            }
        }


        public void Start()
        {
            var retryPolicy =
                Policy.Handle<HttpRequestException>().WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) => {
                        WriteLine($"Failed to fetch events. Current Retry: '{retryCount}'. Exception was '{exception.Message}'.");
                    });
            
            do
            {
                retryPolicy.Execute(() => SubscriptionCycleCallback().GetAwaiter().GetResult());
                Task.Delay(6000).GetAwaiter().GetResult();
            } while (true);
        }
    }
}
