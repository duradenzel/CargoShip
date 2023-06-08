using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip.Writers
{
    public class PlacementWriter
    {
        private readonly CargoLoad cargoLoad;

        public PlacementWriter(CargoLoad cargoLoad)
        {
            this.cargoLoad = cargoLoad;
        }

        public void PrintContainerPlacementOverview()
        {
            Console.WriteLine("\n--- Container Placement Overview ---");
            List<Row> rows = cargoLoad.GetRows();
            for (int row = 0; row < rows.Count; row++)
            {
                Row currentRow = rows[row];
                List<Column> columns = currentRow.GetColumns();
                for (int column = 0; column < columns.Count; column++)
                {
                    Column currentColumn = columns[column];
                    List<Container> localContainers = currentColumn.GetContainers();
                    int containerCount = localContainers.Count;
                    int totalWeight = localContainers.Sum(c => c.Weight);

                    Console.WriteLine($"Row: {row}, Column: {column} | Containers: {containerCount} | Total Weight: {totalWeight}");
                }
            }
        }

        public void PrintBalanceOverview()
        {
            Console.WriteLine("\n--- Balance Overview ---");
            int leftWeight = cargoLoad.GetLeftWeight();
            int rightWeight = cargoLoad.GetRightWeight();
            Console.WriteLine($"{leftWeight} / {rightWeight}");
        }
    }


}
