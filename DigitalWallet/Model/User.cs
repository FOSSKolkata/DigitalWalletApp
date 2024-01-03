using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Model
{
    internal class User
    {
        private readonly string _id;
        private readonly string _name;
        public User(string name)
        {
            this._id = Guid.NewGuid().ToString();
            this._name = name;
        }

        public string Id { get
            {
                return this._id;
            }
        }

        public string Name { 
            get
            {
                return this._name;
            }
        }
    }
}   
