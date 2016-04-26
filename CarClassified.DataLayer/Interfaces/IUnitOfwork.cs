using CarClassified.Models.Views;
using Dapper;
using System;
using System.Collections.Generic;
using static Dapper.SqlMapper;

namespace CarClassified.DataLayer.Interfaces
{
    /// <summary>
    /// Wraps transaction calls
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameter">The parameter.</param>
        void Execute(string query, object parameter = null);

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string query, object parameter = null);

        //TODO: experimen
        GridReader GetAssests(string query, AllAssests all);

        /// <summary>
        /// Multis the map query.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first.</typeparam>
        /// <typeparam name="TSecond">The type of the second.</typeparam>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="map">The map.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        IEnumerable<TReturn> MultiMapQuery<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> map, object parameter = null);
    }
}
