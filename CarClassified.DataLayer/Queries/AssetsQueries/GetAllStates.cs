using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Queries.AssetsQueries
{
    public class GetAllStates : IQuery<ICollection<State>>
    {
        public ICollection<State> Execute(IUnitOfWork unit)
        {
            return unit.Query<State>("SELECT * FROM State").ToList();
        }
    }
}
