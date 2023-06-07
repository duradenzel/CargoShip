using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip
{
    public class Row
    {
        public List<Container> Containers;

        public Row()
        {
            Containers = new List<Container>();
        }

        public void AddContainer(Container container, int columnIndex)
        {
            Containers.Insert(columnIndex, container);
        }

        public bool CanContainerBePlaced(Container container)
        {
            // Container placement validation logic specific to the row
            // Return true if the container can be placed, false otherwise
            return true;
        }

        // Other methods and properties related to row management
    }
}
