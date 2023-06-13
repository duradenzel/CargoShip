using CargoShip.Factories;
using CargoShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip
{
    public class Container
    {
        private static int i = 1;

        public int Id { get; } 

        public int Weight { get; set; } = 4000;
        public int MaxWeight { get;} = 30000;
        public ContentType Content { get; set; }
        public bool IsValuable { get; set; }
        public bool IsRefrigerated { get; set; }
       

        public Container(ContentType content)
        {
            Id = i++;
            Content = content;
            Weight = CalculateWeight(content);
        }

        private int CalculateWeight(ContentType content)
        {
            return Math.Min(Weight + content.ToString().Length * 1000, MaxWeight);
        }
    }



    public enum ContentType
    {
        Contraband,
        Dion,
        Electronics,
        Food,
        Clothes,
        Tools,
        Water,
        WackyWavingInflatableTubeGuy


    }

}
