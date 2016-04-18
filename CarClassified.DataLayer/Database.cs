using CarClassified.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer
{
    public class Database : IDatabase
    {
        private IUnitOfWork _unit;

        public Database(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public void Execute(ICommand command)
        {
            command.Execute(_unit);
        }

        public T Query<T>(IQuery<T> query)
        {
            return query.Execute(_unit);
        }
    }
}