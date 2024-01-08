using DigitalWallet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Data
{
    internal class OfferRepository : IRepository<Offer, int>
    {
        HashSet<Offer> _offers;

        public OfferRepository()
        {
            _offers = new HashSet<Offer>();
        }
        public void Add(Offer entity)
        {
            _offers.Add(entity);
        }

        public Offer Get(int id)
        {
            return _offers.Where(x => x.id == id).SingleOrDefault();
        }

        public List<Offer> GetAll()
        {
            return _offers.ToList();
        }
    }
}
