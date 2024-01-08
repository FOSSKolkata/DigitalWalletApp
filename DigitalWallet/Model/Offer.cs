using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Model
{
    public class Offer
    {
        public int id { get; }
        public string Name { get;  }
        public OfferTrigger Trigger { get;  }
        public DateTime CreatedOn { get; }

        public Offer(int id, string name, OfferTrigger trigger)
        {
            this.id = id;
            Name = name;
            Trigger = trigger;
            CreatedOn = DateTime.UtcNow;
        }
    }

    public enum OfferTrigger
    {
        OnTransaction,
        OnDemand
    }
}
