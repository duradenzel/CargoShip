using CargoShip.Interfaces;
using CargoShip.Writers;
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
        public List<Row> rows { get; private set; }
        private IContainerPlacer containerPlacer;
        private PlacementWriter placementWriter;

        private int LeftWeight;
        private int RightWeight;
        private int MaxWeight;

    

        public CargoLoad(int rows, int columns, IContainerValidator containerValidator, int maxWeight)
        {
            MaxWeight = maxWeight;
            this.rows = new List<Row>();
            containerPlacer = new ContainerPlacer(containerValidator);
            placementWriter = new PlacementWriter(this);

            for (int i = 0; i < rows; i++)
            {
                this.rows.Add(new Row(columns, containerPlacer));
            }

        }

            public List<Row> GetRows()
            {
                return rows;
            }

            public int GetLeftWeight()
            {
                return LeftWeight;
            }

            public int GetRightWeight()
            {
                return RightWeight;
            }

            public void LoadContainers(List<Container> containers)
            {
                List<Container> failedContainers = new List<Container>();

                foreach (Container container in containers)
                {
                PlaceContainer();
                    bool containerLoaded = false;
                    string errorMessage = string.Empty;

                    foreach ((Row row, int rowIndex) in rows.Select((r, index) => (r, index)))
                    {
                        if (row.TryPlaceContainer(container, rowIndex, out errorMessage))
                        {
                        int leftRowCount = rows.Count / 2;
                        int rightRowCount = rows.Count - leftRowCount;

                        if (rowIndex < leftRowCount)
                        {
                            LeftWeight += container.Weight;
                        }
                        else if (rowIndex >= leftRowCount + rightRowCount)
                        {
                            RightWeight += container.Weight;
                        }
                        else
                        {
                            LeftWeight += container.Weight / 2;
                            RightWeight += container.Weight / 2;
                        }

                        containerLoaded = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Container {container.Id} loaded at row {rowIndex} with weight: {container.Weight}.  Refrigerated = {container.IsRefrigerated}. Valuable = {container.IsValuable}");
                            Console.ResetColor();
                        Console.WriteLine($"Current Balance:  Left - {LeftWeight} | Right - {RightWeight}");

                        break;
                        }
                    }

                    if (!containerLoaded)
                    {
                        failedContainers.Add(container);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Unable to load container: {container.Id}. Reason: {errorMessage}");
                        Console.ResetColor();
                    }
                }

                PlacementWriter placementWriter = new PlacementWriter(this);
                placementWriter.PrintContainerPlacementOverview();
                placementWriter.PrintBalanceOverview();

                int totalWeight = containers.Sum(c => c.Weight);
             
                double weightPercentage = (double)totalWeight / MaxWeight;

                Console.WriteLine($"Weight loaded: {totalWeight}. Weight needed to sail: {MaxWeight / 2}");
                if (weightPercentage > 0.5)
                {
                    Console.WriteLine("Container placement successful. Ship is ready to sail.");
                }
                else
                {
                    Console.WriteLine("Not enough weight present on the ship. Ship is unable to sail.");
                }
            }   
    }



}