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
        public string ValidateContainerPlacement(Container container, List<Container>[,] layout, int row, int column)
        {
            string leftOrRight = (layout.GetLength(0) / 2 > row) ? "left" : "right";

            if (container.IsRefrigerated == false)
            {
                if (leftOrRight == "left" && GetLeftWeight(layout) > GetRightWeight(layout))
                    return "Balance is off";
                else if (leftOrRight == "right" && GetRightWeight(layout) > GetLeftWeight(layout))
                    return "Balance is off";
            }

            if (container.IsRefrigerated && row != 0)
                return "Refrigerated containers can only be placed in the first row.";

            int totalWeight = container.Weight;

            foreach (Container localContainer in layout[row, column])
            {
                totalWeight += localContainer.Weight;

                if (totalWeight > 120000)
                    return "Total weight of containers in this position exceeds the maximum weight.";

                if (localContainer.IsValuable)
                    return "A valuable container already exists in this position.";
            }

            return string.Empty;
        }

        private int GetLeftWeight(List<Container>[,] layout)
        {
            int rows = layout.GetLength(0);
            int columns = layout.GetLength(1);
            int leftWeight = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (row < rows / 2)
                    {
                        leftWeight += layout[row, column].Sum(c => c.Weight);
                    }
                }
            }

            return leftWeight;
        }

        private int GetRightWeight(List<Container>[,] layout)
        {
            int rows = layout.GetLength(0);
            int columns = layout.GetLength(1);
            int rightWeight = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (row >= rows / 2)
                    {
                        rightWeight += layout[row, column].Sum(c => c.Weight);
                    }
                }
            }

            return rightWeight;
        }
    }


}
