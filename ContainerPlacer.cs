using CargoShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip
{
    public class ContainerPlacer : IContainerPlacer
    {
        private IContainerValidator containerValidator;

        public ContainerPlacer(IContainerValidator containerValidator)
        {
            this.containerValidator = containerValidator;
        }

        public bool PlaceContainer(Container container, List<Container> containers, int rowIndex, out string errorMessage)
        {
            errorMessage = containerValidator.ValidateContainerPlacement(container, rowIndex, containers);

            if (string.IsNullOrEmpty(errorMessage))
            {
                containers.Add(container);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
