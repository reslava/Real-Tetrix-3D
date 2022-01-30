using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class AutoCleanupSingleton<T> : MonoBehaviour
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
 
    void Awake()
    {
        if (_instance != null)        
            Destroy(this.gameObject); // Prevent duplicates                    
        else        
            DontDestroyOnLoad(gameObject); // Dont destroy the object when change the Scene and come back
        
    }
}

}

// Usage:
// public class XManager : AutoCleanupSingleton<XManager> {}