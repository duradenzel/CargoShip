using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip.Interfaces
{
    public interface IContainerValidator
    {
        string CanContainerBePlaced(Container container, List<Row> rows, int rowIndex, int columnIndex);

    }
}
