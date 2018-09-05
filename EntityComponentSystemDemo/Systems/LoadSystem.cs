using EntityComponentSystemDemo.Components;
using EntityComponentSystemDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityComponentSystemDemo.Systems
{
    public class LoadSystem : BaseSystem
    {
        public LoadSystem(EntityManager entityManager) : base(entityManager)
        {
        }

        public override void Update(double dt)
        {
            foreach (var taxi in EntityManager.GetAllEntities<CargoComponent>())
            {
                if(taxi.Component.People.Count() < taxi.Component.PeopleCapacity)
                {
                    foreach (var person in EntityManager.GetAllEntities<PackageComponent>())
                    {
                        if (person.Component.Status == PackageStatus.Waiting)
                        {
                            var currentPosition = EntityManager.GetComponent<PositionComponent>(taxi.Id);
                            var fromEntityPosition = EntityManager.GetComponent<PositionComponent>(person.Component.FromEntity);
                            var toEntityPosition = EntityManager.GetComponent<PositionComponent>(person.Component.ToEntity);

                            if (currentPosition.X == fromEntityPosition.X && currentPosition.Y == fromEntityPosition.Y)
                            {
                                taxi.Component.People.Add(person.Id);
                                EntityManager.AddComponentToEntity(taxi.Id, new TargetComponent { X = toEntityPosition.X, Y = toEntityPosition.Y });
                                EntityManager.GetComponent<VisualComponent>(taxi.Id).UseAlternativeColor = true;
                                EntityManager.GetComponent<VisualComponent>(person.Component.FromEntity).UseAlternativeColor = false;
                                person.Component.Status = PackageStatus.Moving;
                            }
                        }
                    }
                }
            }
        }
    }
}
