﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Interfaces
{
    public interface IQuery<T>
    {
        T Execute(IUnitOfWork unit);
    }
}