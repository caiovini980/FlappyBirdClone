using System.Collections.Generic;
using UnityEngine;

namespace Utils.PoolingUtils
{
    public class ObjectPool : MonoBehaviour
    {
        // VARIABLES
        private Dictionary<string, Queue<GameObject>> _objectPool = new Dictionary<string, Queue<GameObject>>();

        // METHODS
        public GameObject GetObject(GameObject givenObject)
        {
            if (GetQueueOfGivenObject(givenObject, out Queue<GameObject> objectList))
            {
                if (objectList.Count > 0)
                {
                    GameObject objectFound = objectList.Dequeue();
                    objectFound.SetActive(true);
                    return objectFound;
                }
            }
            
            return CreateNewObject(givenObject);
        }

        public void ReturnObjectToThePool(GameObject givenObject)
        {
            if (GetQueueOfGivenObject(givenObject, out Queue<GameObject> objectList))
            {
                objectList.Enqueue(givenObject);
            }
            else
            {
                Queue<GameObject> newObjectQueue = new Queue<GameObject>();
                newObjectQueue.Enqueue(givenObject);
                _objectPool.Add(givenObject.name, newObjectQueue);
            }
            
            givenObject.SetActive(false);
        }

        private GameObject CreateNewObject(GameObject givenObject)
        {
            GameObject newObject = Instantiate(givenObject);
            newObject.name = givenObject.name;
            return newObject;
        }
        
        private bool GetQueueOfGivenObject(GameObject givenObject, out Queue<GameObject> objectList)
        {
            return _objectPool.TryGetValue(givenObject.name, out objectList);
        }
    }
}
