using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityComponentSystemDemo.Components
{
    public class CargoComponent : Component
    {
        public int PeopleCapacity;
        
        public List<int> People;

        public CargoComponent(int peopleCapacity)
        {
            this.People = new List<int>();
            this.PeopleCapacity = peopleCapacity;
        }
    }
}
