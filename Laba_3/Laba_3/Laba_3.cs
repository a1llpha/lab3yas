
using System;
using System.Collections.Generic;

namespace laba_3
{
    public class ORGANISM
    {
        public int Energy { get; set; }
        public int Age { get; set; }
        public int Size { get; set; }

        public ORGANISM(int energy, int age, int size)
        {
            Energy = energy;
            Age = age;
            Size = size;
        }
    }


    public class Animal : ORGANISM, IReproducible, IPredator
    {
        public string Species { get; set; }

        public Animal(int energy, int age, int size, string species)
            : base(energy, age, size)
        {
            Species = species;
        }

        public void Reproduce()
        {
            Console.WriteLine($"{Species} Reproduce.");
        }

        public void Hunt(ORGANISM prey)
        {
            Console.WriteLine($"{Species} hunting on organism {prey.Energy} unit energy.");
        }
    }

    public class Predator : ORGANISM, IPredator
    {
        public Predator(int energy, int age, int size)
            : base(energy, age, size)
        {
        }

        public int Energy { get; set; } // Реалізація властивості Energy

        public void Hunt(ORGANISM prey)
        {
            Console.WriteLine("Хижак полює на жертву.");
        }
    }

    public class Plant : ORGANISM, IReproducible
    {
        public string Type { get; set; }

        public Plant(int energy, int age, int size, string type)
            : base(energy, age, size)
        {
            Type = type;
        }

        public void Reproduce()
        {
            Console.WriteLine($"{Type} Reproduce.");
        }
    }

    public class Microorganism : ORGANISM, IReproducible
    {
        public string Name { get; set; }

        public Microorganism(int energy, int age, int size, string name)
            : base(energy, age, size)
        {
            Name = name;
        }

        public void Reproduce()
        {
            Console.WriteLine($"{Name} Reproduce.");
        }
    }


    public interface IReproducible
    {
        void Reproduce();
    }


    public interface IPredator
    {
        int Energy { get; set; } 
        void Hunt(ORGANISM prey);
    }
 


    public class Ecosystem
    {
        private List<ORGANISM> organisms;

        public Ecosystem()
        {
            organisms = new List<ORGANISM>();
        }

        public void AddOrganism(ORGANISM organism)
        {
            organisms.Add(organism);
        }

        public void SimulateInteraction()
        {
            foreach (var organism in organisms)
            {
                if (organism is IPredator predator)
                {
                    foreach (var prey in organisms)
                    {
                        if (predator != prey && organism.Energy > prey.Energy)
                        {
                            predator.Hunt(prey);
                            predator.Energy += prey.Energy;
                            organisms.Remove(prey);
                            break;
                        }
                    }
                }

                if (organism is IReproducible reproducer)
                {
                    if (organism.Age < 10)
                    {
                        reproducer.Reproduce();
                        var newOrganism = new ORGANISM(50, 0, 5);
                        organisms.Add(newOrganism);
                    }
                }
            }
        }
    }
public class Computer
{
    public string IPAddress { get; set; }
    public int Power { get; set; }
    public string OperatingSystem { get; set; }

    public Computer(string ipAddress, int power, string operatingSystem)
    {
        IPAddress = ipAddress;
        Power = power;
        OperatingSystem = operatingSystem;
    }

    public virtual void Connect(Computer target)
    {
        Console.WriteLine($"[{IPAddress}] Connected to [{target.IPAddress}]");
    }

    public virtual void Disconnect(Computer target)
    {
        Console.WriteLine($"[{IPAddress}] Disconnected from [{target.IPAddress}]");
    }

    public virtual void SendData(Computer target, string data)
    {
        Console.WriteLine($"[{IPAddress}] Sent data to [{target.IPAddress}]: {data}");
    }

    public virtual void ReceiveData(Computer source, string data)
    {
        Console.WriteLine($"[{IPAddress}] Received data from [{source.IPAddress}]: {data}");
    }
}

public class Server : Computer
{
    public int StorageCapacity { get; set; }

    public Server(string ipAddress, int power, string operatingSystem, int storageCapacity)
        : base(ipAddress, power, operatingSystem)
    {
        StorageCapacity = storageCapacity;
    }
}

public class Workstation : Computer
{
    public string Department { get; set; }

    public Workstation(string ipAddress, int power, string operatingSystem, string department)
        : base(ipAddress, power, operatingSystem)
    {
        Department = department;
    }
}

public class Router : Computer
{
    private List<Computer> connectedComputers = new List<Computer>();

    public Router(string ipAddress, int power, string operatingSystem)
        : base(ipAddress, power, operatingSystem)
    {
    }

    public override void Connect(Computer target)
    {
        base.Connect(target);
        connectedComputers.Add(target);
    }

    public override void Disconnect(Computer target)
    {
        base.Disconnect(target);
        connectedComputers.Remove(target);
    }
}

public class Network
{
    private List<Computer> computers = new List<Computer>();

    public void AddComputer(Computer computer)
    {
        computers.Add(computer);
    }

    public void EstablishConnection(Computer device1, Computer device2)
    {
        device1.Connect(device2);
        device2.Connect(device1);
    }
}

    
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Ecosystem ecosystem = new Ecosystem();
            Animal lion = new Animal(100, 5, 15, "Lion");
            Animal gazelle = new Animal(50, 3, 10, "Газель");
            Plant tree = new Plant(30, 10, 50, "Дерево");
            Microorganism bacteria = new Microorganism(10, 1, 1, "Бактерія");
            ecosystem.AddOrganism(lion);
            ecosystem.AddOrganism(gazelle);
            ecosystem.AddOrganism(tree);
            ecosystem.AddOrganism(bacteria);
            ecosystem.SimulateInteraction();
            */
            Server server = new Server("192.168.1.1", 1000, "Windows Server", 2000);
            Workstation workstation = new Workstation("192.168.1.2", 500, "Windows 10", "HR");
            Router router = new Router("192.168.1.254", 200, "Embedded OS");

            Network network = new Network();
            network.AddComputer(server);
            network.AddComputer(workstation);
            network.AddComputer(router);

            network.EstablishConnection(server, router);
            network.EstablishConnection(workstation, router);

            server.SendData(workstation, "Data from server to workstation");
            workstation.SendData(server, "Data from workstation to server");
        }
        }
    }

