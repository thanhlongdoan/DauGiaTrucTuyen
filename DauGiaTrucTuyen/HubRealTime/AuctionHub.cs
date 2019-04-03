using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Script.Serialization;
using DauGiaTrucTuyen.Data;
using Microsoft.AspNet.SignalR;

namespace DauGiaTrucTuyen.HubRealTime
{
    public class AuctionHub : Hub
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        public void Hello()
        {
            Clients.All.hello();
        }

        public void JoinAuction(string productId, string userId)
        {
            var transaction = db.Transactions.Where(x => x.Product_Id == productId).FirstOrDefault();
            if (transaction != null)
            {

                TransactionAuction transactionAuction = new TransactionAuction();
                transactionAuction.Transaction_Id = transaction.Transaction_Id;
                transactionAuction.User_Id = userId;
                transactionAuction.AuctionDate = DateTime.Now;
                transactionAuction.AuctionPrice = transaction.PriceStart; ;
                db.TransactionAuctions.Add(transactionAuction);
                db.SaveChanges();

                Groups.Add(Context.ConnectionId, transaction.Transaction_Id);
            }
        }

        private Timer _timer;
        private int _timerInterval;
        public void EndAuction()
        {
            _timerInterval = 15000;
        }

        private void StartTimer()
        {
            _timer = new Timer(_timerInterval);

            // ADD HANDLER TO TIMER.ELAPSED EVENT
            _timer.Elapsed += OnTimerElapsed;

            // START THE TIMER GOING
            _timer.Start();
        }


        private void OnTimerElapsed(Object source, ElapsedEventArgs e)
        {
            // IF MAINTENANCE DATETIME IS AN ACTUAL DATETIME VALUE
            //DateTime maintenanceDatetime;
            //if (DateTime.TryParse(MaintenanceRepository.MaintenanceDatetime(), out maintenanceDatetime))
            //{
            //    // DETERMINE HOW MANY MINUTES ARE REMAINING UNTIL MAINTENANCE IS TO OCCUR
            //    var minutesRemaining = Convert.ToInt32(maintenanceDatetime.Subtract(DateTime.Now).TotalMinutes);

            //    // INDICATES THE NUMBER OF MINUTES FROM THE MAINTENANCE OCCURRING TO NOTIFY USERS OF THE MAINTEANCE. 
            //    // THIS WILL MAKE IT SO WE AVOID NOTIFYING USERS OF MAINTENANCE TO START NEXT WEEK OR SO.
            //    var notificationThreshold = MaintenanceRepository.MaintenanceNotificationThreshold();

            //    // IF MINUTESREMAINING IS WITHIN THE NOTIFICATION THRESHOLD
            //    if (minutesRemaining <= notificationThreshold)
            //    {
            //        // GET THE FINAL NOTIFICATION THRESHOLD
            //        var finalNotficationThreshold = MaintenanceRepository.FinalNotificationThreshold();

            //        // CREATE AND POPULATE DICTIONARY TO PASS NAME/VALUES TO CLIENT
            //        Dictionary<string, string> msg = new Dictionary<string, string>();
            //        msg.Add("mr", minutesRemaining.ToString());
            //        msg.Add("nt", notificationThreshold.ToString());
            //        msg.Add("fnt", finalNotficationThreshold.ToString());

            //        // DETERMINE IF DATE IS DAYLIGHTSAVINGS OR STANDARD
            //        var tz = TimeZoneInfo.Local;
            //        var mdt = maintenanceDatetime.ToString("MM/dd/yyyy h:mm tt");
            //        mdt += " " + (tz.IsDaylightSavingTime(maintenanceDatetime) ? tz.DaylightName : tz.StandardName);

            //        // DETERMINE WHICH MESSAGE TO RETURN.  USE INITIAL NOTIFICATION MESSAGE BY DEFAULT
            //        var notficationMessage = MaintenanceRepository.InitialNotificationMessage();
            //        if (minutesRemaining <= finalNotficationThreshold) notficationMessage = MaintenanceRepository.FinalNotificationMessage(); // REPLACE SOME TOKENS notficationMessage = notficationMessage.Replace("##DATETIME##", mdt); // FORM APPROPRIATE REMAINING MINUTES STRING var remaining_minutes_str = minutesRemaining.ToString() + " minute" + (minutesRemaining > 1 ? "s" : "");
            //        notficationMessage = notficationMessage.Replace("##REMAINING_MINUTES##", remaining_minutes_str);

            //        msg.Add("nm", notficationMessage);

            //        // SERIALIZE AS JSON AND BROADCAST TO ALL CLIENTS
            //        string json = new JavaScriptSerializer().Serialize(msg);
            //        Clients.All.maintenanceOccurring(json);
            //    }
            //}
        }

    }
}