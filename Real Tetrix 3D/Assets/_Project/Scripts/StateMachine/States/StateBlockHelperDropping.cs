using UnityEngine;

namespace RafaEslava {

public class StateBlockHelperDropping: IState
{   
    public void Tick() 
    {
        //Debug.Log("Tetrix State BlockDropping Tick *****************");     
        Events.OnBlockHelperDroppingTick?.Invoke();        
    }

    public void OnEnter()
    {
        Debug.Log("Tetrix State BlockDropping Enter *****************");                
    }

    public void OnExit() 
    {
        Debug.Log("Tetrix State BlockDropping Exit *****************");       
        
        Events.OnBlockHelperDroppingExit?.Invoke();        
        
        Conditions.IsBlockDroping = false;           
    }             
}

}