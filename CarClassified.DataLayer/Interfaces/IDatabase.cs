using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Interfaces
{
    public interface IDatabase
    {
        void Execute(ICommand command);

        T Query<T>(IQuery<T> query);
    }
}