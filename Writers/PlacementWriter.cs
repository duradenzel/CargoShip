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
            for (int row = 0; row < cargoLoad.GetRows(); row++)
            {
                for (int column = 0; column < cargoLoad.GetColumns(); column++)
                {
                    List<Container> localContainers = cargoLoad.GetLayout()[row, column];
                    int containerCount = localContainers.Count;
                    int totalWeight = localContainers.Sum(c => c.Weight);

                    Console.WriteLine($"Row: {row}, Column: {column} | Containers: {containerCount} | Total Weight: {totalWeight}");
                }
            }
        }

        public void PrintBalanceOverview()
        {
            Console.WriteLine("\n--- Balance Overview ---");
            Console.WriteLine($"{cargoLoad.GetLeftWeight()} / {cargoLoad.GetRightWeight()}");
        }
    }

}
