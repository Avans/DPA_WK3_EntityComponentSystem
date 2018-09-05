using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityComponentSystemDemo.Entities;
using EntityComponentSystemDemo.Components;

namespace EntityComponentSystemDemo.Systems
{
    public class SpawnPeopleSystem : BaseSystem
    {
        private const int ROUNDS_FOR_NEW_PERSON = 20;
        private int _currentRound = 0;
        private EntityFactory _factory;
        private Random _random;

        public SpawnPeopleSystem(EntityManager entityManager, EntityFactory factory) : base(entityManager)
        {
            _random = new Random();
            this._factory = factory;
        }

        public override void Update(double dt)
        {
            if(++_currentRound == ROUNDS_FOR_NEW_PERSON)
            {
                _currentRound = 0;

                int houseIx1 = _random.Next(0, _factory.Houses.Count);
                int houseIx2 = _random.Next(0, _factory.Houses.Count);

                _factory.CreatePerson("New person", 1, _factory.Houses[houseIx1], _factory.Houses[houseIx2]);
                EntityManager.GetComponent<VisualComponent>(_factory.Houses[houseIx1]).UseAlternativeColor = true;
            }
        }
    }
}
