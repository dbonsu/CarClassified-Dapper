using CarClassified.DataLayer.Base;
using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace CarClassified.DataLayer
{
    /// <summary>
    /// Concrete implementation of
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Interfaces.IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        private ICarClassifiedContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(ICarClassifiedContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameter">The parameter.</param>
        public void Execute(string query, object parameter = null)
        {
            _context.Transaction(t => _context.Connection.Execute(query, parameter, t));
        }

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
        public IEnumerable<TReturn> MultiMapQuery<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> map, object parameter = null)
        {
            return _context.Transaction(t =>
          {
              var result = _context.Connection.Query<TFirst, TSecond, TReturn>(query, map, parameter, t);
              return result;
          });
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string query, object parameter = null)
        {
            return _context.Transaction(t =>
            {
                var result = _context.Connection.Query<T>(query, parameter, t);
                return result;
            });
        }
    }
}