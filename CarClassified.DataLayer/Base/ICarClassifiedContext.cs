using System;
using System.Data;

namespace CarClassified.DataLayer.Base
{
    /// <summary>
    /// Interface for db context
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ICarClassifiedContext : IDisposable
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        IDbConnection Connection { get; }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls the back.
        /// </summary>
        void RollBack();

        /// <summary>
        /// Transactions the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        T Transaction<T>(Func<IDbTransaction, T> query);
    }
}
