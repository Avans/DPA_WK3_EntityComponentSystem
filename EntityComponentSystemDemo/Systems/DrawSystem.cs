using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityComponentSystemDemo.Entities;
using EntityComponentSystemDemo.Components;

namespace EntityComponentSystemDemo.Systems
{
    public class DrawSystem : BaseSystem
    {
        public DrawSystem(EntityManager entityManager) : base(entityManager)
        {
        }

        public override void Update(double dt)
        {
            foreach (var entity in EntityManager.GetAllEntities<VisualComponent>())
            {
                PositionComponent position = EntityManager.GetComponent<PositionComponent>(entity.Id);

                Console.SetCursorPosition(position.X, position.Y);
                Console.BackgroundColor = entity.Component.UseAlternativeColor ? entity.Component.AlternativeColor : entity.Component.Color;
                Console.Write(' ');
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
