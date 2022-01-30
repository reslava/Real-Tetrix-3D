using UnityEngine;
using UnityEngine.UI;

namespace RafaEslava {

public class StateGameOver: IState
{    

    public void Tick() {}
    

    public void OnEnter()
    {        
        Debug.Log("Tetrix StateGameOver Enter *****************");    

        Tools.IsPaused = true;        
        
        // Conditions.IsPlaying = false;
        
        Events.OnGameOverEnter?.Invoke();
    }      
    
    public void OnExit()
    {
        Debug.Log("Tetrix StateGameOver Exit *****************");              		        
    }      
}
}