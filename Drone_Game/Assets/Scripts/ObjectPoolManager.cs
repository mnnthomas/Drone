using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DroneGame
{
    [System.Serializable]
    public class ObjectToPool
    {
        public string _Name;
        public int _Count;
        public GameObject _Object;
        public bool _CanSpawnAboveCount = true;

        public List<GameObject> pPooledObjects { get; private set; }

        public void AddToPool(GameObject obj)
        {
            if (pPooledObjects == null)
                pPooledObjects = new List<GameObject>();

            pPooledObjects.Add(obj);
        }
    }

    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager pInstance { get; private set; } = null;
        public static System.Action OnObjectsPooled = null;

        [SerializeField] private List<ObjectToPool> m_ObjectsToPool = new List<ObjectToPool>();

        private void Awake()
        {
            if(pInstance == null)
                pInstance = this;
        }

        private void OnDestroy()
        {
            pInstance = null;
        }

        private void Start()
        {
            for(int i = 0; i< m_ObjectsToPool.Count; i++)
            {
                for(int j = 0; j < m_ObjectsToPool[i]._Count; j++)
                {
                    GameObject obj = Instantiate(m_ObjectsToPool[i]._Object);
                    m_ObjectsToPool[i].AddToPool(obj);
                    obj.transform.parent = transform;
                    obj.SetActive(false);
                }
            }

            OnObjectsPooled?.Invoke();
        }

        public void AddBackToPool(GameObject obj)
        {
            if (obj == null)
                return;

            for(int i = 0; i < m_ObjectsToPool.Count; i++)
            {
                if(m_ObjectsToPool[i].pPooledObjects.Find(x => x == obj))
                {
                    obj.transform.position = Vector3.zero;
                    obj.transform.rotation = Quaternion.identity;
                    obj.transform.SetParent(transform);
                    obj.SetActive(false);
                    return;
                }
            }
            Debug.Log("The object does not belong to any pooled objects " + obj.name);
        }

        public GameObject SpawnObject(string name, Vector3 pos = default, Quaternion rot = default, Transform parent = null)
        {
            ObjectToPool curPool = m_ObjectsToPool.Find(x => x._Name == name);
            if(curPool != null)
            {
               for(int i = 0; i < curPool.pPooledObjects.Count; i++)
                {
                    GameObject curObj = curPool.pPooledObjects[i];
                    if (!curObj.activeInHierarchy)
                    {
                        curObj.SetActive(true);
                        curObj.transform.position = pos;
                        curObj.transform.rotation = rot;
                        curObj.transform.SetParent(parent);
                        return curObj;
                    }
                    else if((i == curPool.pPooledObjects.Count -1) && curObj.activeInHierarchy && curPool._CanSpawnAboveCount)
                    {
                        GameObject obj = Instantiate(curPool._Object);
                        curPool.AddToPool(obj);

                        obj.transform.position = pos;
                        obj.transform.rotation = rot;
                        obj.transform.SetParent(parent);
                        return obj;
                    }
                }
            }
            else
            {
                Debug.Log("Object with name "+name+" not found in ObjectPool list");
            }

            return null;
        }

    }
}


