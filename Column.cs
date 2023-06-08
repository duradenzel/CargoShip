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

        public bool TryPlaceContainer(Container container, out string errorMessage)
        {
            return containerPlacer.PlaceContainer(container, containers, out errorMessage);
        }
    }

}
