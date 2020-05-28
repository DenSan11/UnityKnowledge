using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : FSingleton.SingletonMono<ObjectPool> {

    private GameObject cachePoor;

    private Dictionary<string, Queue<GameObject>> m_Pool = new Dictionary<string, Queue<GameObject>>();

    private Dictionary<GameObject, string> m_GoTag = new Dictionary<GameObject, string>();//唯一标记

    /// <summary>
    /// 清空缓存池，释放所有引用
    /// </summary>
    public void ClearCachePool()
    {
        m_Pool.Clear();
        m_GoTag.Clear();
    }

    /// <summary>
    /// 回收GameObject
    /// </summary>
    public void ReturnCacheGameObejct(GameObject go)
    {
        if (cachePoor == null)
        {
            cachePoor = new GameObject();
            cachePoor.name = "CachePoor";
            GameObject.DontDestroyOnLoad(cachePoor);
        }

        if (go == null)
        {
            return;
        }

        go.transform.parent = cachePoor.transform;
        go.SetActive(false);

        if (m_GoTag.ContainsKey(go))
        {
            string tag = m_GoTag[go];
            RemoveOutMark(go);

            if (!m_Pool.ContainsKey(tag))
            {
                m_Pool[tag] = new Queue<GameObject>();
            }

            m_Pool[tag].Enqueue(go);
        }
    }

    /// <summary>
    /// 请求GameObject
    /// </summary>
    public GameObject RequestCacheGameObejct(GameObject prefab)
    {
        string tag = prefab.GetInstanceID().ToString();//获取对象ID
        
        GameObject go = GetFromPool(tag);
        if (go == null)
        {
            go = GameObject.Instantiate<GameObject>(prefab);
            go.name = prefab.name +":" + tag;
        }

        MarkAsOut(go, tag);
        return go;
    }


    private GameObject GetFromPool(string tag)
    {
        if (m_Pool.ContainsKey(tag) && m_Pool[tag].Count > 0)
        {
            GameObject obj = m_Pool[tag].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 取出时添加标记
    /// </summary>
    /// <param name="go"></param>
    /// <param name="tag"></param>
    private void MarkAsOut(GameObject go, string tag)
    {
        m_GoTag.Add(go, tag);
    }

    /// <summary>
    /// 放回是去除标记
    /// </summary>
    /// <param name="go"></param>
    private void RemoveOutMark(GameObject go)
    {
        if (m_GoTag.ContainsKey(go))
        {
            m_GoTag.Remove(go);
        }
        else
        {
            Debug.LogError("remove out mark error, gameObject has not been marked");
        }
    }
}
