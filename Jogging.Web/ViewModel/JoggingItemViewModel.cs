using System;

namespace Jogging.Web.ViewModel
{
    public class JoggingItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double Distance { get; set; }

        public string AverageSpeed => $"{(Distance/1000)/Time.Hours} km/h";
    }
}