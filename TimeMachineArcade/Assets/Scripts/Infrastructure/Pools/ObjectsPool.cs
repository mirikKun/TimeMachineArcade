using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public class ObjectsPool<T> where T : Transform
    {
        private T _prefab;
        private readonly Transform _parent;

        private T _elementPrefab;

        private int _initialPoolSize = 3;

        private int _poolSize;


        private readonly List<T> _elements = new();
        private List<T> _activeElements = new();

        public List<T> ActiveElements => _activeElements;

        private void GenerateNewElement()
        {
            T newElements = Object.Instantiate(_prefab, _parent);
            newElements.gameObject.SetActive(false);
            _elements.Add(newElements);
        }

        public  ObjectsPool(T prefab, Transform parent, int maxCount)
        {
            if (_prefab == null)
            {
                Debug.LogError("Need a reference to the destination prefab");
            }

            _prefab = prefab;
            _parent = parent;
            _poolSize = maxCount;
            for (int i = 0; i < _initialPoolSize; i++)
            {
                GenerateNewElement();
            }
        }

        public void RevertAllToPool()
        {
            foreach (var element in _activeElements)
            {
                element.gameObject.SetActive(false);
            }

            _activeElements.Clear();
        }
        public void RevertToPool(T element)
        {
            element.gameObject.SetActive(false);
            
            _activeElements.Remove(element);
        }

        public T GetElement()
        {
            foreach (T destination in _elements)
            {
                if (!destination.gameObject.activeInHierarchy)
                {
                    destination.gameObject.SetActive(true);

                    _activeElements.Add(destination);
                    return destination;
                }
            }

            while (_elements.Count >= _poolSize)
            {
                _poolSize++;
            }

            GenerateNewElement();
            T lastDestination = _elements[^1];

            lastDestination.gameObject.SetActive(true);
            _activeElements.Add(lastDestination);

            return lastDestination;
        }
    }
}