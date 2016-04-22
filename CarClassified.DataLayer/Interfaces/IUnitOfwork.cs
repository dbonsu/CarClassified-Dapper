using CarClassified.Models.Views;
using Dapper;
using System;
using System.Collections.Generic;
using static Dapper.SqlMapper;

namespace CarClassified.DataLayer.Interfaces
{
    public interface IUnitOfWork
    {
        void Execute(string query, object parameter = null);

        IEnumerable<T> Query<T>(string query, object parameter = null);

        GridReader GetAssests(string query, AllAssests all);

        IEnumerable<TReturn> MultiMapQuery<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> map, object parameter = null);
    }
}
