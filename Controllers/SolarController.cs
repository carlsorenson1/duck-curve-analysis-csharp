using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace duck_curve_analysis_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolarController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Sample> Get()
    {
        var solarSamples = new List<Sample>
        {
            new Sample("2020-12-01T00:00:00", 0),
            new Sample("2020-12-01T01:30:00", 0),
            new Sample("2020-12-01T01:00:00", 0),
            new Sample("2020-12-01T00:30:00", 0),
            new Sample("2020-12-01T02:00:00", 0),
            new Sample("2020-12-01T02:30:00", 0),
            new Sample("2020-12-01T03:00:00", 0),
            new Sample("2020-12-01T03:30:00", 0),
            new Sample("2020-12-01T04:00:00", 0),
            new Sample("2020-12-01T04:30:00", 0),
            new Sample("2020-12-01T05:00:00", 0),
            new Sample("2020-12-01T05:30:00", 0),
            new Sample("2020-12-01T06:00:00", 0),
            new Sample("2020-12-01T06:30:00", 0),
            new Sample("2020-12-01T07:00:00", 0),
            new Sample("2020-12-01T07:30:00", 50),
            new Sample("2020-12-01T08:00:00", 150),
            new Sample("2020-12-01T08:30:00", 300),
            new Sample("2020-12-01T09:00:00", 700),
            new Sample("2020-12-01T09:30:00", 1200),
            new Sample("2020-12-01T10:00:00", 1700),
            new Sample("2020-12-01T10:30:00", 2000),
            new Sample("2020-12-01T11:00:00", 2150),
            new Sample("2020-12-01T11:30:00", 2250),
            new Sample("2020-12-01T12:00:00", 2300),
            new Sample("2020-12-01T12:30:00", 2350),
            new Sample("2020-12-01T13:00:00", 2350),
            new Sample("2020-12-01T13:30:00", 2350),
            new Sample("2020-12-01T14:00:00", 2300),
            new Sample("2020-12-01T14:30:00", 2250),
            new Sample("2020-12-01T15:00:00", 2150),
            new Sample("2020-12-01T15:30:00", 2000),
            new Sample("2020-12-01T16:00:00", 1700),
            new Sample("2020-12-01T16:30:00", 1200),
            new Sample("2020-12-01T17:00:00", 700),
            new Sample("2020-12-01T17:30:00", 300),
            new Sample("2020-12-01T18:00:00", 150),
            new Sample("2020-12-01T18:30:00", 50),
            new Sample("2020-12-01T19:00:00", 0),
            new Sample("2020-12-01T19:30:00", 0),
            new Sample("2020-12-01T20:00:00", 0),
            new Sample("2020-12-01T20:30:00", 0),
            new Sample("2020-12-01T21:00:00", 0),
            new Sample("2020-12-01T21:30:00", 0),
            new Sample("2020-12-01T22:00:00", 0),
            new Sample("2020-12-01T22:30:00", 0),
            new Sample("2020-12-01T23:00:00", 0),
            new Sample("2020-12-01T23:30:00", 0)
        };

        return solarSamples.ToArray();
    }

}
