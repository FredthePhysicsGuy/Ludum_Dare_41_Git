using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingletonManager<T> : MonoBehaviour where T : Component
{

    static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();
                if (m_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    m_instance = obj.AddComponent<T>();
                }
            }
            return m_instance;
        }
    }

    public virtual void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
