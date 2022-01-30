using UnityEngine;

namespace RafaEslava {

public class StateLevelUp: IState
{
    public void Tick() 
    {
        //Debug.Log("Tetrix State Tick *****************");
    }

    public void OnEnter()
    {
        Debug.Log("Tetrix State LevelUp Enter *****************");   

        Tools.IsPaused = true;
        Events.OnLevelUpEnter?.Invoke();                    
    }      
    
    public void OnExit()
    {
        Debug.Log("Tetrix State LevelUp Exit *****************");              		
        Tools.IsPaused = false;
    }      
}

}