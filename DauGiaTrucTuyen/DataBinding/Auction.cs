using System;

namespace DauGiaTrucTuyen.DataBinding
{
    public class Auction
    {
        public double ProductPrice { get; set; }
        public int TimeRemaining
        {
            get
            {
                var a = (int)Math.Abs(GetTimeRemaining().TotalSeconds);
                return (int)Math.Abs(GetTimeRemaining().TotalSeconds);
            }
        }

        public string TypeTime { get; set; }
        public int IncrementAmountPerBid { get; set; }
        public double BidPrice { get; set; }
        public int BidsTotal { get; set; }
        public DateTime LastBid { get; set; }
        public DateTime EndTime { get; set; }

        public decimal ValueLastBid { get; set; }
        public decimal ValueNextBid { get { return ValueLastBid + (decimal)0.01; } }
        public string LastUserBid { get; set; }

        public string EndTimeFullText
        {
            get { return string.Format("{0:MM/dd/yyyy HH\\:mm\\:ss}", EndTime); }
        }

        public Auction() { }

        public Auction(int bidsTotal, int incrementAmountPerBid, DateTime endTime, decimal valueInitialBid)
        {
            BidsTotal = bidsTotal;
            IncrementAmountPerBid = incrementAmountPerBid;
            LastBid = DateTime.Now;
            EndTime = endTime;
            ValueLastBid = valueInitialBid;
        }

        public TimeSpan GetTimeElapsed()
        {
            return DateTime.Now.Subtract(LastBid);
        }

        public TimeSpan GetTimeRemaining()
        {
            var a = DateTime.Now.Subtract(EndTime);
            return DateTime.Now.Subtract(EndTime);
        }

        public void SetEndTime()
        {
            EndTime = LastBid.Add(GetTimeElapsed().Add(TimeSpan.FromSeconds(IncrementAmountPerBid)));
        }

        public void PlaceBid(decimal valueLastBid, string lastUserBid)
        {
            ValueLastBid = valueLastBid;
            LastUserBid = lastUserBid;
            BidsTotal++;
            LastBid = DateTime.Now;
            SetEndTime();
        }
    }
}