using UnityEngine;

namespace RafaEslava {

public class StateBlockHelperRotating: IState
{     
    public void Tick() 
    {
        //Debug.Log("Tetrix State BlockRotating Tick *****************");
        Events.OnBlockHelperRotatingTick?.Invoke();
    }

    public void OnEnter()
    {
        Debug.Log("Tetrix State BlockRotating Enter *****************");
        Conditions.IsBlockHelperRotating = true;

        Events.OnBlockHelperRotatingEnter?.Invoke();              
    }

    public void OnExit() 
    {
        Debug.Log("Tetrix State BlockRotating Exit *****************");       
    }      
}

}