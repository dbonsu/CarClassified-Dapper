using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Queries
{
    public class DebugQuery : IQuery<ICollection<Color>>
    {
        public ICollection<Color> Execute(IUnitOfWork unit)
        {
            return unit.Query<Color>("SELECT * FROM Color").ToList();
        }
    }
}