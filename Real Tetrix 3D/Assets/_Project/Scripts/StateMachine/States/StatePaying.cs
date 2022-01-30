using UnityEngine;

namespace RafaEslava {

public class StatePlaying: IState
{     
    // Para evitar movimiento o rotacion al pasar presionando el boton play en el menu al empezar a jugar
    // No procesamos el Input del primer Tick/Update
    private int FranesCount; 
    public void Tick() 
    {
        // Debug.Log("Tetrix State Playing Tick *****************");        
		FranesCount++;
        if(FranesCount>1)
        {
            // Classes used in game during playing time has no update, they are called in Playing.Tick        
            Events.OnPlayingTick?.Invoke();						        
        }
    }

    public void OnEnter()
    {
        FranesCount = 0;
        Debug.Log("Tetrix State Playing Enter *****************");      
        Input.ResetInputAxes(); 
    }

    public void OnExit() 
    {
        Debug.Log("Tetrix State Paying Exit *****************");

        if(Conditions.IsSettings)
            Conditions.IsGameStart = false;
    }      
}

}