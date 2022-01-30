using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RafaEslava {

public class InputController : MonoBehaviour
{             
	public GameData gameData;
    private Move blockMove;    
	private Rotate blockRotate;    
	private Drop blockDrop;    
	private Planes planes;
    private GridOverlay grid;   

	public Button ButtonBlockDrop; 

    private const float STEP = 0.2f;

#if (UNITY_ANDROID)		
	// private Vector2 startTouchPosition = Vector2.zero;
#endif

#region MONOBEHAVIOURS EVENTS ***********************************************************

	private void OnEnable() 
	{
		Events.OnPlayingTick.AddListener(OnPlayingTick);			
	}

	private void OnDisable() 
	{
		Events.OnPlayingTick.RemoveListener(OnPlayingTick);			
	} 

    private void Awake() 
    {        		
        blockMove = GameObject.FindObjectOfType<Move>();   
		blockRotate = GameObject.FindObjectOfType<Rotate>();   
		blockDrop = GameObject.FindObjectOfType<Drop>();   
		planes = GameObject.FindObjectOfType<Planes>();
        grid = GameObject.FindObjectOfType<GridOverlay>();    		
    }

#endregion

    public void OnPlayingTick()
    {
		if(blockMove.BlockHelper != null)		
		{			
			GameObject blockHelper = blockMove.BlockHelper;
			Vector3 blockHelperPosition = blockMove.Position;	


#region BLOCK MOVE **********************************************************************
		//***********************************************
		// Move Block			
		//***********************************************
#if (UNITY_ANDROID)		
		// float deltaX = 0, deltaY = 0;	
		// // Si es dispositivo móvil y se ha arrastrado el toque y terminado
		// if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
		// 	// Guardamos la posición inicial
		// 	startTouchPosition = Input.GetTouch(0).position;


		// // Si es dispositivo móvil y el primer toque es en la mitad izquierda de la pantalla
        // if (startTouchPosition.x <= Screen.width / 2) { 
		// 	// Si se ha arrastrado el toque y hay terminado
        //     if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
		// 		// Y si ha recorrido mas de 50 puntos
		// 		if(Vector2.Distance(startTouchPosition, Input.GetTouch(0).position) > 50) {								
		// 			// Mover el bloque segun el ángulo
		// 			deltaX = Input.GetTouch(0).position.x - startTouchPosition.x;
		// 			deltaY = Input.GetTouch(0).position.y - startTouchPosition.y;
		// 			float limit = 50f;
		// 			if(deltaX < -limit) 
		// 				blockMove.MoveLeft();
		// 			else if(deltaX > limit) 
		// 				blockMove.MoveRight();
					
		// 			if(deltaY > limit) 
		// 				blockMove.MoveUp();
		// 			else if(deltaY < -limit) 
		// 				blockMove.MoveDown();
		// 		}
		// 	}
		// }
#else
		if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			blockMove.MoveLeft();
		}		
		if(Input.GetKeyDown(KeyCode.RightArrow)) {		
			blockMove.MoveRight();
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)) {		
			blockMove.MoveUp();
		}		
		if(Input.GetKeyDown(KeyCode.DownArrow)) {	
			blockMove.MoveDown();
		}

#endif

		// Block new position adjusting				
		
		if (blockHelperPosition.x < -(grid.Size / 2 + 5) * STEP)
			blockMove.PositionNext.x += STEP;
		if (blockHelperPosition.x > (grid.Size / 2 + 5) * STEP)
			blockMove.PositionNext.x -= STEP;
		if (blockHelperPosition.z < -(grid.Size / 2 + 5) * STEP)
			blockMove.PositionNext.z += STEP;
		if (blockHelperPosition.z > (grid.Size / 2 + 5) * STEP)
			blockMove.PositionNext.z -= STEP;
		if (blockHelperPosition.y < 0.1f)
			blockMove.PositionNext.y = 0.1f;
		
		if (blockHelperPosition.y > 0.1f + planes.PlaneGetY(gameData.maxYStart))
			blockMove.PositionNext.y -= STEP;			


		// Block helper move	
		
		blockMove.MoveToPositionNext();
		//blockHelperController.BlockHelper.transform.position = blockHelperController.PositionNext;
		//blockHelper.gameObject.transform.position = new Vector3 (blockHelperController.xTo, blockHelperController.yTo, blockHelperController.zTo);


		//BlockHelperNext_SetOnCorner ();

		if (Input.GetKeyDown (KeyCode.A)) blockMove.MoveYUp();		
		if (Input.GetKeyDown (KeyCode.Z)) blockMove.MoveYDown();
		
#endregion

#region BLOCK EXTRAS ********************************************************************
		//*********************************************************************************
		// Detectar bloque posicionable y colisiones
		// Oscurecer o devolver color al bloque
		// TODO: Habiliar/deshabilitar boton drop
		//*********************************************************************************
		// // // // Color c;
		// // // // //!!!!!
		// // // // //c = gameController.Block_GetColor (gameController.iBlock);
		// // // // c = blockHelperController.BlockHelper.GetComponent<BlockSetup>().Color; 		
		ButtonBlockDrop.interactable = !Collission.IsBlockCollision;
		// // // // if (!BlockCollissionHelper.IsBlockCollision) 
		// // // // {
		// // // // 	foreach(Renderer render in blockHelperController.BlockHelper.GetComponentsInChildren<Renderer>()) 
		// // // // 	{
		// // // // 		render.material.color = c;
		// // // // 	}
		// // // // } else 
		// // // // {
		// // // // 	c.r -= 0.5f;
		// // // // 	c.g -= 0.5f;
		// // // // 	c.b -= 0.5f;
		// // // // 	foreach(Renderer render in blockHelperController.BlockHelper.GetComponentsInChildren<Renderer>()) 
		// // // // 	{
		// // // // 		render.material.color = c;
		// // // // 	}			
		// // // // }			
#endregion

#region BLOCK DROP **********************************************************************
#if (UNITY_ANDROID)
#else
		//***********************************************
		//New block
		if (Input.GetKeyDown(KeyCode.RightControl)) 
		{
			// if (!BlockCollissionHelper.IsBlockCollision) 			
			// 	Conditions.IsBlockDroping = true;
			BlockDrop();
			
			return;
		}
#endif
#endregion

#region BLOCK ROTATE ********************************************************************
#if (UNITY_ANDROID)		
		//***********************************************
		// Rotate block		
		//***********************************************
		
        // if(startTouchPosition.x > Screen.width / 2) { 						
		// 	if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
		// 		if(Vector2.Distance(startTouchPosition, Input.GetTouch(0).position) > 50) {						
		// 			deltaX = Input.GetTouch(0).position.x - startTouchPosition.x;
		// 			deltaY = Input.GetTouch(0).position.y - startTouchPosition.y;
					
		// 			float limit = 50f;							

		// 			if(deltaY > limit && deltaX > limit) {
		// 				blockRotate.RotateForward(90f);
		// 			} else if(deltaY < -limit && deltaX < -limit) {
		// 				blockRotate.RotateForward(-90f);
		// 			} else if(deltaX > limit) {
		// 				blockRotate.RotateUp(90f);
		// 			} else	if(deltaX < -limit) {
		// 				blockRotate.RotateUp(-90f);
		// 			} else if(deltaY > limit) {
		// 				blockRotate.RotateRight(90f);
		// 			} else if(deltaY < -limit) {
		// 				blockRotate.RotateRight(-90f);
		// 			} 			
		// 		}
		// 	}
		// }
#else	
		if (Input.GetKeyDown (KeyCode.Q)) blockRotate.RotateUp(-90f);		
		if (Input.GetKeyDown (KeyCode.W)) blockRotate.RotateUp(90f);
		if (Input.GetKeyDown (KeyCode.E)) blockRotate.RotateRight(90f);		
		if (Input.GetKeyDown (KeyCode.D)) blockRotate.RotateRight(-90f);
		if (Input.GetKeyDown (KeyCode.T)) blockRotate.RotateForward(90f);
		if (Input.GetKeyDown (KeyCode.R)) blockRotate.RotateForward(-90f);			
#endif
#endregion
        }
        
    }	

	public void BlockDrop()
	{
		if (!Collission.IsBlockCollision) 			
			Conditions.IsBlockDroping = true;
	}
}

}