using EntityComponentSystemDemo.Components;
using EntityComponentSystemDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityComponentSystemDemo.Systems
{
    public class UnloadSystem : BaseSystem
    {
        public UnloadSystem(EntityManager entityManager) : base(entityManager)
        {
        }

        public override void Update(double dt)
        {
            foreach (var taxi in EntityManager.GetAllEntities<CargoComponent>())
            {
                List<int> droppedOfPeople = new List<int>();
                foreach (var personId in taxi.Component.People)
                {
                    var person = EntityManager.GetComponent<PackageComponent>(personId);
                    var toEntityPosition = EntityManager.GetComponent<PositionComponent>(person.ToEntity);
                    var currentPosition = EntityManager.GetComponent<PositionComponent>(taxi.Id);

                    if(toEntityPosition.X == currentPosition.X && toEntityPosition.Y == currentPosition.Y)
                    {
                        person.Status = PackageStatus.Delivered;
                        EntityManager.RemoveComponentFromEntity<TargetComponent>(taxi.Id);
                        EntityManager.GetComponent<VisualComponent>(taxi.Id).UseAlternativeColor = false;
                        EntityManager.RemoveEntity(personId);
                        droppedOfPeople.Add(personId);
                    }
                }

                droppedOfPeople.ForEach(i => taxi.Component.People.Remove(i));
            }
        }
    }
}
