using CargoShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoShip.Factories
{

    public class ContainerFactory : IContainerFactory
    {
        private static Random random = new Random();

        public Container CreateRandomContainer()
        {
            ContentType contentType = GetRandomContentType();

            bool isValuable = random.Next(2) == 0; 
            bool isRefrigerated = random.Next(2) == 0;

            return new Container(contentType)
            {
                Content = contentType,
                IsValuable = isValuable,
                IsRefrigerated = isRefrigerated,             
            };
        }

        private static ContentType GetRandomContentType()
        {
            Array values = Enum.GetValues(typeof(ContentType));
            return (ContentType)values.GetValue(random.Next(values.Length));
        }
    }



}

