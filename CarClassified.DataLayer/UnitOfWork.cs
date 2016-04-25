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
    public class UnitOfWork : IUnitOfWork
    {
        private ICarClassifiedContext _context;

        public UnitOfWork(ICarClassifiedContext context)
        {
            _context = context;
        }

        public void Execute(string query, object parameter = null)
        {
            _context.Transaction(t => _context.Connection.Execute(query, parameter, t));
        }

        public GridReader GetAssests(string query, AllAssests all)
        {
            var reader =

              _context.Transaction(
               t =>
               {
                   var result = _context.Connection.QueryMultiple(query, null, t);
                   all.BodyStyles = result.Read<BodyStyle>().ToList();
                   all.Colors = result.Read<Color>().ToList();
                   all.Conditions = result.Read<Condition>().ToList();
                   all.Cylinders = result.Read<Cylinder>().ToList();
                   all.Transmissions = result.Read<Transmission>().ToList();
                   return result;
               });

            return reader;
        }

        public IEnumerable<TReturn> MultiMapQuery<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> map, object parameter = null)
        {
            return _context.Transaction(t =>
          {
              var result = _context.Connection.Query<TFirst, TSecond, TReturn>(query, map, parameter, t);
              return result;
          });
        }

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