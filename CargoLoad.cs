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
        private List<Container>[,] layout;
        private IContainerPlacer containerPlacer;
        private readonly PlacementWriter placementWriter;


        private int LeftWeight { get; set; }
        private int RightWeight { get; set; }
        private const double Balance = 0.2;
        private int Rows { get; }
        private int Columns { get; }

        public int GetRows()
        {
            return Rows;
        }

        public int GetColumns()
        {
            return Columns;
        }

        public List<Container>[,] GetLayout()
        {
            return layout;
        }

        public int GetLeftWeight()
        {
            return LeftWeight;
        }

        public int GetRightWeight()
        {
            return RightWeight;
        }


        //We geven de containerValidator mee in de constructor van de CargoLoad zodat hij ontkoppelt is van de cargoclass zelf
        //Aangezien de cargoload niet zelf intern iets creëert op instantieert, maar hij laat de vorige class (Ship) de implementatie
        //van de validator kiezen. zo kunnen we makkelijk switchen van implementatie indien nodig (bijvoorbeeld een andere validator)

        public CargoLoad(int rows, int columns, IContainerValidator containerValidator)
        {
            Rows = rows;
            Columns = columns;
            layout = new List<Container>[Rows, Columns];
            InitializeLayout();

            containerPlacer = new ContainerPlacer(containerValidator);
            placementWriter = new PlacementWriter(this);
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
                string errorMessage = string.Empty;

                for (int row = 0; row < Rows; row++)
                {
                    for (int column = 0; column < Columns; column++)
                    {
                        if (containerPlacer.PlaceContainer(container, layout, row, column, out errorMessage))
                        {
                            if (Rows / 2 > row)
                            {
                                LeftWeight += container.Weight;
                            }
                            else
                            {
                                RightWeight += container.Weight;
                            }

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
                    Console.WriteLine($"Unable to load container: {container.Id}. Reason: {errorMessage}");
                    Console.ResetColor();
                }
            }

            
            placementWriter.PrintContainerPlacementOverview();
            placementWriter.PrintBalanceOverview();
        }
    }


}