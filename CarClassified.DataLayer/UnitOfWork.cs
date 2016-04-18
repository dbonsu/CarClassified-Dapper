﻿using CarClassified.DataLayer.Base;
using CarClassified.DataLayer.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _context.Transaction(transaction => _context.Connection.Execute(query, parameter, transaction));
        }

        public IEnumerable<T> Query<T>(string query, object parameter = null)
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query<T>(query, parameter, transaction);
                return result;
            });
        }
    }
}