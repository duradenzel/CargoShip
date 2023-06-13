using CargoShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip
{
    public class Column
    {
        private List<Container> containers;
        private IContainerPlacer containerPlacer;

        public Column(IContainerPlacer containerPlacer)
        {
            containers = new List<Container>();
            this.containerPlacer = containerPlacer;
        }

        public List<Container> GetContainers()
        {
            return containers;
        }

        public int GetTotalWeight()
        {
            return containers.Sum(c => c.Weight);
        }

        public bool TryPlaceContainer(Container container, int rowIndex, out string errorMessage)
        {
            return containerPlacer.PlaceContainer(container, containers, rowIndex, out errorMessage);
        }
    }

}
