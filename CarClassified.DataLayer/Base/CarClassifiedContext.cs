﻿using CarClassified.Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CarClassified.DataLayer.Base
{
    /// <summary>
    /// Concrete class for db context
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Base.ICarClassifiedContext" />
    public class CarClassifiedContext : ICarClassifiedContext
    {
        private IDbConnection _connection;
        private string _connectionString;

        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarClassifiedContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public CarClassifiedContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new SqlConnection(_connectionString);

                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.ConnectionString = _connectionString ?? BaseSettings.ConnectionString;
                    _connection.Open();
                }

                return _connection;
            }
        }

        /// <summary>
        /// Gets or sets the _transaction.
        /// </summary>
        /// <value>
        /// The _transaction.
        /// </value>
        private IDbTransaction _transaction { get; set; }

        public IDbTransaction BeginTransaction()
        {
            if (_transaction == null || _transaction.Connection == null)
            {
                _transaction = Connection.BeginTransaction();
            }
            return _transaction;
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <exception cref="System.NullReferenceException">Attempted to commit on closed transaction</exception>
        public void Commit()
        {
            try
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
            catch (Exception e)
            {
                if (_transaction != null && _transaction.Connection != null)
                {
                    RollBack();
                    //add logging
                    throw new NullReferenceException("Attempted to commit on closed transaction");
                }
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Rolls the back.
        /// </summary>
        /// <exception cref="System.NullReferenceException">Tried Rollback on closed Transaction</exception>
        public void RollBack()
        {
            try
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
            catch (Exception ex)
            {
                //add logging
                throw new NullReferenceException("Tried Rollback on closed Transaction", ex);
            }
        }

        /// <summary>
        /// Transactions the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public T Transaction<T>(Func<IDbTransaction, T> query)
        {
            using (var connection = Connection)
            {
                using (var transaction = BeginTransaction())
                {
                    try
                    {
                        var result = query(transaction);
                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        RollBack();
                        throw;
                    }
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _connection != null)
                {
                    _connection.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}
