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
    private readonly PlacementWriter placementWriter;

        private int LeftWeight;
        private int RightWeight;

    

    public CargoLoad(int rows, int columns, IContainerValidator containerValidator)
    {
        this.rows = new List<Row>();

        for (int i = 0; i < rows; i++)
        {
            this.rows.Add(new Row(columns));
        }

        containerPlacer = new ContainerPlacer(containerValidator);
        placementWriter = new PlacementWriter(this);
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
            bool containerLoaded = false;
            string errorMessage = string.Empty;

            foreach (Row row in rows)
            {
                if (row.TryPlaceContainer(container, out errorMessage))
                {
                    if (rows.IndexOf(row) < rows.Count / 2)
                    {
                        LeftWeight += container.Weight;
                    }
                    else
                    {
                        RightWeight += container.Weight;
                    }

                    containerLoaded = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Container {container.Id} loaded at row {rows.IndexOf(row)} with weight: {container.Weight}.  Refrigerated = {container.IsRefrigerated}. Valuable = {container.IsValuable}");
                    Console.ResetColor();
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
    }
}



}