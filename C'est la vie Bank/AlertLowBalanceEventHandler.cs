using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_est_la_vie_Bank
{
      public  class AlertLowBalanceEventHandler
      {

        Account acc;
        string message;
        private decimal amount;
        public AlertLowBalanceEventHandler(Account acc , string message  )
        {
             this.message = message;
            this.acc = acc;
        
             

        }

    }
}
