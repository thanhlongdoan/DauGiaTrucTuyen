using System;

namespace DauGiaTrucTuyen.DataBinding
{
    public class Auction
    {
        public int TimeRemaining
        {
            get
            {
                return (int)Math.Abs(GetTimeRemaining().TotalSeconds);
            }
        }
        
        public DateTime EndTime { get; set; }

        public Auction() { }

        public Auction(DateTime endTime)
        {
            EndTime = endTime;
        }

        public TimeSpan GetTimeRemaining()
        {
            return DateTime.Now.Subtract(EndTime);
        }
    }
}