using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_est_la_vie_Bank
{
    internal class Notifications
    {
        public Notifications() { }  
        public void ProccessCompleted(object?sender , ProccesCompletedEventArgs e)
        {
            Console.WriteLine($" Procces Success {e.message}");
        }
        public void ProccessFailed( object ? sender , TransactionFailedEventArgs e) 
        {
        
        
        }
        public void LowBalanceDetected(object? sender, AlertLowBalanceEventHandler e)
        {

        }
        public void Subscribe(Account account)
        {
            
            account.TransictionField += ProccessFailed;
            account.ProccesCompleted += ProccessCompleted;
            account.LowBalanceDetected += LowBalanceDetected;
        }

      
    }
}
