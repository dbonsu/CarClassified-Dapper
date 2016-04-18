using System.Collections.Generic;

namespace CarClassified.DataLayer.Interfaces
{
    public interface IUnitOfWork
    {
        void Execute(string query, object parameter = null);

        IEnumerable<T> Query<T>(string query, object parameter = null);
    }
}