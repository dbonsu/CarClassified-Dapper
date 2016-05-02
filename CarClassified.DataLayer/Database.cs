using CarClassified.DataLayer.Interfaces;

namespace CarClassified.DataLayer
{
    /// <summary>
    /// Concrete class for IDatabase -wraps
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Interfaces.IDatabase" />
    public class Database : IDatabase
    {
        private IUnitOfWork _unit;

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        /// <param name="unit">The unit.</param>
        public Database(IUnitOfWork unit)
        {
            _unit = unit;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Execute(ICommand command)
        {
            command.Execute(_unit);
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public T Query<T>(IQuery<T> query)
        {
            return query.Execute(_unit);
        }
    }
}
