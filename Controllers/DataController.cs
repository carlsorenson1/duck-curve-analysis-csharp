using Microsoft.AspNetCore.Mvc;

namespace duck_curve_analysis_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private const string DateFormat = "yyyy-MM-ddTHH:mm:ss";

        private static readonly Dictionary<(string, DateTime), List<Sample>> CachedSamples = new();

        [HttpGet("{type}/average/{mode}/{date}")]
        public async Task<List<Sample>> GetAverage(string type, string mode, string date)
        {
            if (!DateTime.TryParse(date, out var currentDate))
            {
                throw new Exception("Invalid date.");
            }

            var samples = new List<Sample>(49);
            var tempDate = currentDate;
            for (var i = 0; i < 49; i++)
            {
                samples.Add(new Sample(tempDate.ToString(DateFormat), 0));
                tempDate = tempDate.AddMinutes(30);
            }

            var startMonth = currentDate.Month;
            var daysIncluded = 0;
            while (currentDate.Month == startMonth)
            {
                var isWeekend = currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday;
                var include = mode switch
                {
                    "all" => true,
                    "weekends" => isWeekend,
                    "weekdays" => !isWeekend,
                    _ => throw new Exception("Invalid mode.")
                };

                if (include)
                {
                    var temp = await GetDay(type, currentDate.ToString(DateFormat));
                    daysIncluded += 1;
                    for (int i = 0; i < 48; i++)
                    {
                        samples[i].AveragePowerWatts += temp[i].AveragePowerWatts;
                    }
                }
                currentDate = currentDate.AddDays(1);
            }

            for (int i = 0; i < 48; i++)
            {
                samples[i].AveragePowerWatts /= daysIncluded;
            }

            return samples;
        }

        [HttpGet("{type}/day/{date}")]
        public async Task<List<Sample>> GetDay(string type, string date)
        {
            var feedId = type switch
            {
                "total" => Environment.GetEnvironmentVariable("FEED_TOTAL"),
                "wh" => Environment.GetEnvironmentVariable("FEED_WH"),
                "car" => Environment.GetEnvironmentVariable("FEED_CAR"),
                "hvac" => Environment.GetEnvironmentVariable("FEED_HVAC"),
                _ => throw new Exception("Unknown feed type")
            };
            if (!DateTime.TryParse(date, out var dateParsed))
            {
                throw new Exception("Invalid date.");
            }
            
            var key = (feedId!, dateParsed);
            if (CachedSamples.TryGetValue(key, out var samples))
            {
                return samples;
            }

            var tzi = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            long GetUnixTime(DateTime dateTime)
            {
                dateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, tzi);
                var result = (long)dateTime.Subtract(DateTime.UnixEpoch).TotalSeconds;
                return result;
            }

            var httpClient = new HttpClient();

            var nextDay = dateParsed.AddDays(1);
            var apiKey = Environment.GetEnvironmentVariable("API_KEY");
            var url = $"https://emoncms.org/feed/average.json?id={feedId}&start={GetUnixTime(dateParsed)}&end={GetUnixTime(nextDay)}&interval=1800&apikey={apiKey}";
            var resp = await httpClient.GetAsync(url);
            resp.EnsureSuccessStatusCode();
            var dataPoints = (await resp.Content.ReadFromJsonAsync<double?[][]>())!;

            var results = new List<Sample>(49);
            for (var i = 0; i < 49; i++)
            {
                var time = DateTime.UnixEpoch.AddMilliseconds(dataPoints[i][0] ?? 0);
                time = TimeZoneInfo.ConvertTimeFromUtc(time, tzi);
                var sample = new Sample(time.ToString(DateFormat), (int?)dataPoints[i][1] ?? 0);
                results.Add(sample);
            }

            CachedSamples.Add(key, results);

            return results;
        }
    }
}
