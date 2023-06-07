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
        public string CanContainerBePlaced(Container container, List<Row> rows, int rowIndex, int columnIndex)
        {
            // Validation logic here

            return "";
        }
    }
}
