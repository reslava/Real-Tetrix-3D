
using UnityEngine;

namespace RafaEslava {

public abstract class Cube : MonoBehaviour
{
    [SerializeField] protected string _id;
    public string Id => _id;    

    [SerializeField] protected GameData _gameData;    

    public abstract void PlaneDoneAction();         
}

}
