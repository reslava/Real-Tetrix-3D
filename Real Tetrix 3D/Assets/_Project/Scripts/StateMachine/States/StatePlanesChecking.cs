using UnityEngine;

namespace RafaEslava {

public class StatePlanesChecking: IState
{
    private GameInitializer gameController;  
    private Planes planesManager;  

    public StatePlanesChecking ()//GameController gameController)
    {        
        planesManager = GameObject.FindObjectOfType<Planes>();       
    }

    public void Tick() 
    {
        //Debug.Log("Tetrix State PlanesChecking Tick *****************");
    }

    public void OnEnter()
    {
        Debug.Log("Tetrix State PlanesChecking Enter *****************");              		
        planesManager.PlanesCheck();        
    }      
    
    public void OnExit()
    {
        Debug.Log("Tetrix State PlanesChecking Exit *****************");             		        
        Collission.IsBlockCollision = false;        
    }      
}

}