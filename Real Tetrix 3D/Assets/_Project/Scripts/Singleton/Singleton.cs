using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    
    private void Awake() 
    {
        Instance = GameObject.FindObjectOfType<T>();
    }
}

}