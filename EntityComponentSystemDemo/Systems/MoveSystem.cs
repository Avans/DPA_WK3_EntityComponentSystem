using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityComponentSystemDemo.Entities;
using EntityComponentSystemDemo.Components;

namespace EntityComponentSystemDemo.Systems
{
    public class MoveSystem : BaseSystem
    {
        public MoveSystem(EntityManager entityManager) : base(entityManager)
        {
        }

        public override void Update(double dt)
        {
            foreach (var entityTowardsTarget in EntityManager.GetAllEntities<TargetComponent>())
            {
                PositionComponent currentPosition = EntityManager.GetComponent<PositionComponent>(entityTowardsTarget.Id);
                MoveComponent move = EntityManager.GetComponent<MoveComponent>(entityTowardsTarget.Id);

                int curX = currentPosition.X;
                int tarX = entityTowardsTarget.Component.X;
                int diffX = tarX - curX;
                if (diffX < 0 && tarX < curX - move.Velocity) currentPosition.X -= move.Velocity;
                else if (diffX > 0 && tarX > curX + move.Velocity) currentPosition.X += move.Velocity;
                else currentPosition.X = entityTowardsTarget.Component.X;

                int curY = currentPosition.Y;
                int tarY = entityTowardsTarget.Component.Y;
                int diffY = tarY - curY;
                if (diffY < 0 && tarY < curY - move.Velocity) currentPosition.Y -= move.Velocity;
                else if (diffY > 0 && tarY > curY + move.Velocity) currentPosition.Y += move.Velocity;
                else currentPosition.Y = entityTowardsTarget.Component.Y;
            }
        }
    }
}
