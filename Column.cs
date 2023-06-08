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

        public Column()
        {
            containers = new List<Container>();
        }

        public List<Container> GetContainers()
        {
            return containers;
        }

        public bool TryPlaceContainer(Container container, out string errorMessage)
        {
            
            errorMessage = string.Empty;

            if (container.IsRefrigerated && containers.Count > 0)
            {
                errorMessage = "Refrigerated containers can only be placed in an empty column.";
                return false;
            }

            int totalWeight = containers.Sum(c => c.Weight) + container.Weight;
            if (totalWeight > 120000)
            {
                errorMessage = "Total weight of containers in this position exceeds the maximum weight.";
                return false;
            }

            if (containers.Any(c => c.IsValuable))
            {
                errorMessage = "A valuable container already exists in this position.";
                return false;
            }

            containers.Add(container);
            return true;
        }


    }
}
