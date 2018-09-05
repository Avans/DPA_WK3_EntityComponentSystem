using EntityComponentSystemDemo.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityComponentSystemDemo.Entities
{
    public class EntityManager
    {
        private int _lowestUnassignedEntityId = -1;
        private List<int> _entities;

        private Dictionary<string, Component[]> _componentsByClass;

        public EntityManager()
        {
            _componentsByClass = new Dictionary<string, Component[]>();
            _entities = new List<int>();
        }

        public int CreateEntity(params Component[] components)
        {
            _entities.Add(++_lowestUnassignedEntityId);

            foreach (var c in components)
            {
                AddComponentToEntity(_lowestUnassignedEntityId, c);
            }

            return _lowestUnassignedEntityId;
        }

        public void AddComponentToEntity(int entityId, Component component)
        {
            string componentType = component.GetType().Name;
            if (!_componentsByClass.ContainsKey(componentType))
            {
                _componentsByClass[componentType] = new Component[10000];
            }

            _componentsByClass[componentType][entityId] = component;
        }

        public void RemoveComponentFromEntity<T>(int entityId) where T : Component
        {
            _componentsByClass[typeof(T).Name][entityId] = null;
        }

        public T GetComponent<T>(int entityId) where T : Component
        {
            string name = typeof(T).Name;
            if (_componentsByClass.ContainsKey(name))
                return (T)_componentsByClass[name][entityId];
            else
                return null;
        }

        public IEnumerable<EntityWithComponent<T>> GetAllEntities<T>() where T : Component
        {
            return _componentsByClass[typeof(T).Name]
                .Select((c, index) => new EntityWithComponent<T>(index, c as T))
                .Where(e => e.Component != null);
        }

        public void RemoveEntity(int entityId)
        {
            _entities.Remove(entityId);
            foreach (var componentCollection in _componentsByClass.Values)
            {
                componentCollection[entityId] = null;
            }
        }


    }
}
