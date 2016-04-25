using CarClassified.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Base
{
    public class CarClassifiedContext : ICarClassifiedContext
    {
        private static readonly PropertyInfo ConnectionInfo = typeof(SqlConnection).GetProperty("InnerConnection", BindingFlags.NonPublic | BindingFlags.Instance);
        private IDbConnection _connection;
        private string _connectionString;

        private bool disposedValue = false;

        public CarClassifiedContext(string connectionString)
        {
            _connectionString = connectionString;
        }

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

        private IDbTransaction _transaction { get; set; }

        public IDbTransaction BeginTransaction()
        {
            if (_transaction == null || _transaction.Connection == null)
            {
                _transaction = Connection.BeginTransaction();
            }
            return _transaction;
        }

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
                    throw new NullReferenceException("Attempted to commit on closed transaction");
                }
            }
        }

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
                throw new NullReferenceException("Tried Rollback on closed Transaction", ex);
            }
        }

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

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _connection != null)
                {
                    // TODO: dispose managed state (managed objects).
                    _connection.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        private static bool GetTransaction(IDbConnection conn)
        {
            var internalConn = ConnectionInfo.GetValue(conn, null);
            var currentTransactionProperty = internalConn.GetType().GetProperty("CurrentTransaction", BindingFlags.NonPublic | BindingFlags.Instance);
            var currentTransaction = currentTransactionProperty.GetValue(internalConn, null);
            var realTransactionProperty = currentTransaction.GetType().GetProperty("Parent", BindingFlags.NonPublic | BindingFlags.Instance);
            var realTransaction = realTransactionProperty.GetValue(currentTransaction, null);
            if (currentTransaction == null) { return false; }
            return true;
            // return (SqlTransaction)realTransaction;
        }

        #region IDisposable Support

        // To detect redundant calls
        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CarClassifiedContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        #endregion IDisposable Support
    }
}
