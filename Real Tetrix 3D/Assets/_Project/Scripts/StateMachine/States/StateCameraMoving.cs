using UnityEngine;

namespace RafaEslava {

public class StateCameraMoving: IState
{
    private MainCameraController mainCameraController;

    public StateCameraMoving ()//CameraController mainCameraController)
    {
        mainCameraController = GameObject.FindObjectOfType<MainCameraController>();        
    }

    public void Tick() 
    {
        //Debug.Log("Tetrix State CameraMoving Tick *****************");
        mainCameraController.CameraMove();
    }

    public void OnEnter()
    {
        Debug.Log("Tetrix State CameraMoving Enter *****************");              		
        
    }      
    
    public void OnExit()
    {
        Debug.Log("Tetrix State CameraMoving Enter *****************");              		
        
    }      
}

}