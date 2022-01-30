using UnityEngine;
using UnityEngine.UI;

namespace RafaEslava {

public class StatePlaneDone: IState
{        
    public void Tick() 
    {        
        //Debug.Log("Tetrix State PlaneDone Tick *****************");
        
        Events.OnPlaneDoneTick?.Invoke();        								
    }

    public void OnEnter()
    {
        Debug.Log("Tetrix State PlaneDone Enter *****************");            

        Events.OnPlaneDoneEnter?.Invoke();        
    }      
    
    public void OnExit()
    {
        Debug.Log("Tetrix State PlaneDone Exit *****************");              		                

        Events.OnPlaneDoneExit?.Invoke();             
    }      
}

}