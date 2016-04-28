using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace CarClassified.DataLayer.Queries.AssetsQueries
{
    /// <summary>
    /// Retrieves all assests for listing selection
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Interfaces.IQuery{CarClassified.Models.Views.AllAssests}" />
    public class GetAllAssests : IQuery<AllAssests>
    {
        /// <summary>
        /// Executes the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns></returns>
        public AllAssests Execute(IUnitOfWork unit)
        {
            AllAssests all = new AllAssests();
            var lookup = new Dictionary<int, Make>();
            var body = "SELECT * FROM BodyStyle";
            var color = "SELECT * FROM Color";
            var condition = "SELECT * FROM Condition";
            var cylinder = "SELECT * FROM Cylinder";

            var trans = "SELECT * FROM Transmission";
            var fuelTypes = "SELECT * FROM Fuel ";
            string makeAndModel = "SELECT * from Make m left join Model vm on vm.MakeId =m.Id Order By m.Name ";
            //string sql = @"SELECT * from BodyStyle;
            //               SELECT * from Color;
            //               SELECT* from Condition;
            //               SELECT* from Cylinder;
            //               SELECT* from State;
            //               SELECT* from Transmission";

            //unit.GetAssests(sql, all);

            all.BodyStyles = unit.Query<BodyStyle>(body).ToList();
            all.Colors = unit.Query<Color>(color).ToList();
            all.Conditions = unit.Query<Condition>(condition).ToList();
            all.Cylinders = unit.Query<Cylinder>(cylinder).ToList();

            all.Transmissions = unit.Query<Transmission>(trans).ToList();
            all.FuelTypes = unit.Query<Fuel>(fuelTypes).ToList();
            unit.MultiMapQuery<Make, Model, Make>(makeAndModel, (make, model) =>
            {
                Make found;
                if (!lookup.TryGetValue(make.Id, out found))
                {
                    lookup.Add(make.Id, found = make);
                }
                if (found.Models == null)
                {
                    found.Models = new List<Model>();
                }
                found.Models.Add(model);
                return found;
            }
            ).AsQueryable();

            all.Makes = lookup.Values;

            return all;
        }
    }
}
