using CargoShip.Factories;
using CargoShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip
{
    public class Harbor
    {
        public List<Container> Containers = new();       

        public void CreateContainers(int amount)
        {
            List<Container> containers = new();
            for (int i = 0; i < amount; i++)
            {
                
                containers.Add(ContainerFactory.CreateRandomContainer());
            }
            containers = containers.OrderBy(c =>
            {
                if (c.IsRefrigerated && !c.IsValuable)
                    return 0;
                else if (c.IsRefrigerated && c.IsValuable)
                    return 1;
                else if (!c.IsRefrigerated && c.IsValuable)
                    return 3;
                else
                    return 2;
            }).ToList();
            this.Containers = containers;

        }
        public Ship CreateShip(int rows, int columns) {
            Ship ship = new(rows, columns);
            return ship;
        }
        
    }

   
}
