using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityComponentSystemDemo.Entities;
using EntityComponentSystemDemo.Components;

namespace EntityComponentSystemDemo.Systems
{
    public class SearchForPassengerSystem : BaseSystem
    {
        public SearchForPassengerSystem(EntityManager entityManager) : base(entityManager)
        {
        }

        public override void Update(double dt)
        {
            foreach (var entity in EntityManager.GetAllEntities<CargoComponent>())
            {
                var target = EntityManager.GetComponent<TargetComponent>(entity.Id);
                if (target == null)
                {
                    var currentLocation = EntityManager.GetComponent<PositionComponent>(entity.Id);
                    var closest =
                        (from person in EntityManager.GetAllEntities<PackageComponent>()
                         where person.Component.Status == PackageStatus.Waiting
                         select new
                         {
                             Person = person,
                             Location = EntityManager.GetComponent<PositionComponent>(person.Component.FromEntity)
                         })
                        .Select(waitingPerson => new {
                            Location = waitingPerson.Location,
                            Distance = Math.Abs(waitingPerson.Location.X - currentLocation.X) + Math.Abs(waitingPerson.Location.Y - currentLocation.Y)
                        })
                        .OrderBy(x => x.Distance)
                        .FirstOrDefault();

                    if(closest != null){
                        EntityManager.AddComponentToEntity(entity.Id, new TargetComponent { X = closest.Location.X, Y = closest.Location.Y });
                    }
                }
            }
        }
    }
}
