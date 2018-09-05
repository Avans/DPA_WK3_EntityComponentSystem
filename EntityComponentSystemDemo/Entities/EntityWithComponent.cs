using EntityComponentSystemDemo.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityComponentSystemDemo.Entities
{
    public struct EntityWithComponent<T> where T : Component
    {
        public int Id;
        public T Component;

        public EntityWithComponent(int entityId, T component)
        {
            this.Id = entityId;
            this.Component = component;
        }
    }
}
