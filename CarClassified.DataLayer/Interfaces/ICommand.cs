using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Interfaces
{
    public interface ICommand
    {
        void Execute(IUnitOfWork unit);
    }
}