using _3D_Rendering_and_Transformation.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Rendering_and_Transformation.Managers
{
    public class ComponentManager
    {
        private static ComponentManager cm;
        private uint nextid;
        private Dictionary<Type, Dictionary<uint, IComponent>> Components = new Dictionary<Type, Dictionary<uint, IComponent>>();

        internal object EntityComponent<T>(object key)
        {
            throw new NotImplementedException();
        }

        private ComponentManager(){}

        public static ComponentManager Get
        {
            get {
                if (cm == null)
                {
                    cm = new ComponentManager();
                }
                return cm;
            }
        }

        public uint NewEntity()
        {
            return nextid++;
        }
        public T EntityComponent<T>(uint id)
        {
            if (!Components.ContainsKey(typeof(T)))
            {
                Components.Add(typeof(T), new Dictionary<uint, IComponent>());
            }
            var components = Components[typeof(T)];
            if (components.ContainsKey(id))
            {
                return (T)components[id];
            }
            else
            {
                return default(T);
            }
        }
        public Dictionary<uint, IComponent> GetComponents<TComponentType>()
        {
            if (!Components.ContainsKey(typeof(TComponentType)))
            {
                Components.Add(typeof(TComponentType), new Dictionary<uint, IComponent>());
            }
            return Components[typeof(TComponentType)];
        }

        public void RemoveComponents()
        {
            Components.Clear();
        }

        public void AddComponentsToEntity(IComponent component, uint id)
        {
            if (!Components.ContainsKey(component.GetType()))
            {
                Components.Add(component.GetType(), new Dictionary<uint, IComponent>());
            }
            var components = Components[component.GetType()];
            components.Add(id, component);
        }

        public void ClearComponents()
        {
            Components.Clear();
        }
    }
}
