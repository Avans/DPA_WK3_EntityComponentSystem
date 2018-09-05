using EntityComponentSystemDemo.Components;
using EntityComponentSystemDemo.Entities;
using EntityComponentSystemDemo.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntityComponentSystemDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 50;

            EntityManager manager = new EntityManager();
            EntityFactory factory = new EntityFactory(manager);

            CreateEntities(manager, factory);
            BaseSystem[] systems = CreateSystems(manager, factory);

            while (true)
            {
                Console.Clear();
                foreach (var system in systems)
                {
                    system.Update(0);
                }
                Thread.Sleep(100);
            }
        }

        private static void CreateEntities(EntityManager manager, EntityFactory factory)
        {
            factory.CreateTaxi(1, 1);
            factory.CreateTaxi(0, 45);

            factory.CreateHouse(30, 10);
            factory.CreateHouse(10, 40);
            factory.CreateHouse(48, 48);
            factory.CreateHouse(25, 25);
            factory.CreateHouse(5, 35);
            factory.CreateHouse(2, 2);
            factory.CreateHouse(30, 30);

            factory.CreatePerson("Henk", 1, factory.Houses[0], factory.Houses[1]);
            manager.GetComponent<VisualComponent>(factory.Houses[0]).UseAlternativeColor = true;
            factory.CreatePerson("Piet", 1, factory.Houses[2], factory.Houses[0]);
            manager.GetComponent<VisualComponent>(factory.Houses[2]).UseAlternativeColor = true;
        }

        private static BaseSystem[] CreateSystems(EntityManager manager, EntityFactory factory)
        {
            return new BaseSystem[]
            {
                new SearchForPassengerSystem(manager),
                new LoadSystem(manager),
                new UnloadSystem(manager),
                new MoveSystem(manager),

                new SpawnPeopleSystem(manager, factory),
                new DrawSystem(manager)
            };
        }
    }
}
