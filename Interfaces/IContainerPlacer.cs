using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip.Interfaces
{
    public interface IContainerPlacer
    {
        bool PlaceContainer(Container container, List<Container> containers, int rowIndex, out string errorMessage);
    }


}
