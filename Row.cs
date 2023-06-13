using CargoShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip
{
    public class Row
    {
        private List<Column> columns;

        public Row(int columnCount)
        {         
            columns = new List<Column>();
            for (int i = 0; i < columnCount; i++)
            {
                columns.Add(new Column());
            }
        }

        public List<Column> GetColumns()
        {
            return columns;
        }

        public bool TryPlaceContainer(Container container, int rowIndex, out string errorMessage)
        {       
                foreach (Column column in columns)
                {
                    if (column.TryPlaceContainer(container, rowIndex, out errorMessage))
                    {
                        return true;
                    }
                }

                errorMessage = "Unable to place the container in any column of the row.";
                return false;        
        }
    }
}
