using CargoShip;
using CargoShip.Factories;
using CargoShip.Interfaces;

IContainerFactory containerFactory = new ContainerFactory(); 

Harbor harbor = new Harbor(containerFactory); 

int r, c, a;
bool validInput = false;

do
{
    Console.WriteLine("Insert row amount:");
    validInput = int.TryParse(Console.ReadLine(), out r);
} while (!validInput);

do
{
    Console.WriteLine("Insert column amount:");
    validInput = int.TryParse(Console.ReadLine(), out c);
} while (!validInput);

do
{
    Console.WriteLine("Insert amount of containers to load:");
    validInput = int.TryParse(Console.ReadLine(), out a);
} while (!validInput);

harbor.CreateContainers(a);
Ship ship = harbor.CreateShip(r, c);
ship.Cargo.LoadContainers(harbor.Containers);

