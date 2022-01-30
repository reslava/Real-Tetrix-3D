using System;
using UnityEngine;


namespace RafaEslava {

public class StateMenu: IState
{               
    public void Tick() 
    {
        //Debug.Log("Tetrix State Menu Tick *****************");        
    }

    public void OnEnter()
    {        
        Debug.Log("Tetrix State Menu Enter *****************");             
        Conditions.IsGameStart = true;
        Tools.IsPaused = true;                        		
        Events.OnMenuEnter?.Invoke();        
    }

    public void OnExit() 
    {
        Debug.Log("Tetrix State Menu Exit *****************");
        Tools.IsPaused = false;          
        if(!Conditions.IsSettings)        
        {
            Conditions.IsGameStart = false;    
            Events.OnGameStart?.Invoke();          
        }
    }      
}

}