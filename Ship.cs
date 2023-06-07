using CargoShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip
{
    public class Ship
    {
        public int MaxWeight { get; }
        public int Rows { get; }
        public int Columns { get; }

        public CargoLoad Cargo { get; }

        public Ship(int rows, int columns, IContainerValidator containerValidator, IContainerPlacer containerPlacer)
        {
            Rows = rows;
            Columns = columns;
            MaxWeight = rows * columns * 150000;

            Cargo = new CargoLoad(rows, columns, containerValidator, containerPlacer);
        }
    }







}
