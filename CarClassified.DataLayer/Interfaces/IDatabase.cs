namespace CarClassified.DataLayer.Interfaces
{
    /// <summary>
    /// Wraps db
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        void Execute(ICommand command);

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        T Query<T>(IQuery<T> query);
    }
}
