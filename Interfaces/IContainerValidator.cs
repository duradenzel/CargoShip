using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip.Interfaces
{
    public interface IContainerValidator
    {
        string ValidateContainerPlacement(Container container, List<Container>[,] layout, int row, int column);

    }
}
