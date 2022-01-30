using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class LazyInstantiatedSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{    
    private static T _instance;

    public static T Instance 
    {
        get 
        {
            if (_instance == null) 
            {
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null)
                    _instance = new GameObject(name: "Instance Of " + typeof(T)).AddComponent<T>();
            }
            return _instance;
        }
    }   
}

}
// Usage:   
// public class XManager : LazyInstantiatedSingleton<XManager> {}
