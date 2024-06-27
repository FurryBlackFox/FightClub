using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace Code.Services
{
    public class ServiceLocator
    {
        public static ServiceLocator Instance { get; private set; }

        private List<object> _registeredServices = new();

        public ServiceLocator()
        {
            Instance = this;
        }

        public T GetService<T>()
        {
            return GetServices<T>().FirstOrDefault();
        }
        
        public IEnumerable<T> GetServices<T>()
        {
            foreach (var serviceObject in _registeredServices)
            {
                if(serviceObject is Object unityObject && !unityObject)
                    continue;
                
                if (serviceObject is T service)
                    yield return service;
            }
        }

        public bool TryRegisterService<T>(T instance) where T : class
        {
            if (_registeredServices.Any(s => s == instance))
                return false;
            
            _registeredServices.Add(instance);

            return true;
        }
    }
}