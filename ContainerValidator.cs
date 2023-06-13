using CargoShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip
{

    public class ContainerValidator : IContainerValidator
    {
        public string ValidateContainerPlacement(Container container, int rowIndex, List<Container> containers)
        {
            if (container.IsRefrigerated && rowIndex != 0)
            {
                return "Refrigerated containers can only be placed in the first row.";
            }

            int totalWeight = containers.Sum(c => c.Weight) + container.Weight;
            if (totalWeight > 120000)
            {
                return "Total weight of containers in this position exceeds the maximum weight.";
            }

            if (containers.Any(c => c.IsValuable))
            {
                return "A valuable container already exists in this position.";
            }

            return string.Empty;
        }
    }


}
