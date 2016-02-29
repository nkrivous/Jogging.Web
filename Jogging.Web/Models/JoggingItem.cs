using System;

namespace Jogging.Web.Models
{
    public class JoggingItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double Distance { get; set; }
        
        public virtual ApplicationUser User { get; set; }
    }
}