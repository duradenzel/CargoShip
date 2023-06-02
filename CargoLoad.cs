using CargoShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip
{
    public class CargoLoad
    {
        private List<Container>[,] layout;


        private int LeftWeight { get; set; } = 8000;
        private int RightWeight { get; set; } = 5000;

        private const double Balance = 0.2;
        private int Rows { get; }
        private int Columns { get; }

        public CargoLoad(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            layout = new List<Container>[Rows, Columns];
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    layout[row, column] = new List<Container>();
                }
            }
        }

        public void LoadContainers(List<Container> containers)
        {
            List<Container> failedContainers = new List<Container>();

            foreach (Container container in containers)
            {
                bool containerLoaded = false;
                string failureReason = string.Empty;

                for (int row = 0; row < Rows; row++)
                {
                    for (int column = 0; column < Columns; column++)
                    {
                        failureReason = CanContainerBePlaced(container, row, column);

                        if (string.IsNullOrEmpty(failureReason))
                        {
                            layout[row, column].Add(container);
                            containerLoaded = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Container {container.Id} loaded at row {row}, column {column} with weight: {container.Weight}.  Refrigerated = {container.IsRefrigerated}. Valuable = {container.IsValuable}");
                            Console.ResetColor();
                            break;
                        }
                    }

                    if (containerLoaded)
                        break;
                }

                if (!containerLoaded)
                {
                    failedContainers.Add(container);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Unable to load container: {container.Id}. Reason: {failureReason}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\n--- Containers Unable to be Loaded ---");
            foreach (Container container in failedContainers)
            {
                Console.WriteLine($"Container: {container.Id}");
            }

            Console.WriteLine("\n--- Container Placement Overview ---");
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    List<Container> localContainers = layout[row, column];
                    int containerCount = localContainers.Count;
                    int totalWeight = localContainers.Sum(c => c.Weight);

                    Console.WriteLine($"Row: {row}, Column: {column} | Containers: {containerCount} | Total Weight: {totalWeight}");
                }
            }
        }


        private string CanContainerBePlaced(Container container, int row, int column)
        {
            if (container.IsRefrigerated && row != 0)
                return "Refrigerated containers can only be placed in the first row.";

            int totalWeight = container.Weight;

            foreach (Container localContainers in layout[row, column])
            {
                totalWeight += localContainers.Weight;

                if (totalWeight > 120000)
                    return "Total weight of containers in this position exceeds the maximum weight.";

                if (localContainers.IsValuable)
                    return "A valuable container already exists in this position.";

                
            }

            return string.Empty; 
        }













    }

}
