using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Base
{
    public interface ICarClassifiedContext : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction BeginTransaction();

        void Commit();

        void RollBack();

        T Transaction<T>(Func<IDbTransaction, T> query);
    }
}