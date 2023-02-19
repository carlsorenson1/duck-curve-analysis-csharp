namespace duck_curve_analysis_csharp;

public class Sample
{
    public string StartTime { get; set; }
    public int AveragePowerWatts { get; set; }

    public Sample(string startTime, int averagePowerWatts)
    {
        StartTime = startTime;
        AveragePowerWatts = averagePowerWatts;
    }
}
