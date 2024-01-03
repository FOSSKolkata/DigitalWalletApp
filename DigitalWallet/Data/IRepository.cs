﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Data
{
    internal interface IRepository<T> where T : class
    {
        T Get(int id);

        List<T> GetAll();

        void Add(T entity);
    }
}
