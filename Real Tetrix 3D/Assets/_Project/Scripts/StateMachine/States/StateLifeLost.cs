using UnityEngine;

namespace RafaEslava {

public class StateLifeLost: IState
{
    public void Tick() 
    {
        //Debug.Log("Tetrix State LifeLost Tick *****************");
    }

    public void OnEnter()
    {
        Debug.Log("Tetrix State LifeLost Enter *****************");   

        Tools.IsPaused = true;
        Events.OnLifeLostEnter?.Invoke();                    
    }      
    
    public void OnExit()
    {
        Debug.Log("Tetrix State LifeLost Exit *****************");              		
        Tools.IsPaused = false;
    }      
}

}