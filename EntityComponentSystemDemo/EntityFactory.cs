using EntityComponentSystemDemo.Components;
using EntityComponentSystemDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityComponentSystemDemo
{
    public class EntityFactory
    {
        private EntityManager _entityManager;
        public List<int> Houses;

        public EntityFactory(EntityManager entityManager)
        {
            this._entityManager = entityManager;
            Houses = new List<int>();
        }

        public int CreateTaxi(int x, int y)
        {
            return _entityManager.CreateEntity(
                new PositionComponent { X = x, Y = y },
                new CargoComponent(5) { PeopleCapacity = 1 },
                new MoveComponent {  Velocity = 1 },
                new VisualComponent { Color = ConsoleColor.Yellow, AlternativeColor = ConsoleColor.White }
            );
        }

        public int CreatePerson(string name, int numberOfCargo, int fromEntity, int toEntity)
        {
            return _entityManager.CreateEntity(
                new PackageComponent { FromEntity = fromEntity, ToEntity = toEntity }
            );
        }



        public int CreateHouse(int x, int y)
        {
            int house = _entityManager.CreateEntity(
                new PositionComponent { X = x, Y = y },
                new VisualComponent { Color = ConsoleColor.Magenta, AlternativeColor = ConsoleColor.Red }
            );

            Houses.Add(house);
            return house;
        }
    }
}
