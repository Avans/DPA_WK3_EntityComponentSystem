using EntityComponentSystemDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityComponentSystemDemo.Systems
{
    public abstract class BaseSystem
    {
        protected EntityManager EntityManager { get; set; }

        public BaseSystem(EntityManager entityManager)
        {
            this.EntityManager = entityManager;
        }

        public abstract void Update(double dt);
    }
}
