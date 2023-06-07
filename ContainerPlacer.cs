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

        public bool PlaceContainer(Container container, List<Container>[,] layout, int row, int column, out string errorMessage)
        {
            errorMessage = containerValidator.ValidateContainerPlacement(container, layout, row, column);

            if (string.IsNullOrEmpty(errorMessage))
            {
                layout[row, column].Add(container);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
