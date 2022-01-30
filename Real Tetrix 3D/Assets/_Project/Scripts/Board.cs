using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {
	
public class Board : MonoBehaviour
{    	
    public GameData gameData;	
    public GameObject Border;	

    private MainCameraController mainCameraController;
	private Spawner blockSpawner;
	private Move blockMove;
    private GridOverlay grid;	

    private GameObject borderBottom, borderLeft, borderRight, borderBehind, borderFront;		

    #region MONOBEHAVIOR EVENTS **************************************************************	
	
    private void OnEnable() 
	{
		Events.OnLevelUpEnter.AddListener(BoardInitialize);
		Events.OnLifeLostEnter.AddListener(BoardInitialize);
	}	

	private void OnDisable() 
	{
		Events.OnLevelUpEnter.RemoveListener(BoardInitialize);
		Events.OnLifeLostEnter.RemoveListener(BoardInitialize);
	}

    private void Awake() 
    {    					
		mainCameraController = GameObject.FindObjectOfType<MainCameraController>();		
		blockSpawner = GameObject.FindObjectOfType<Spawner>();
		blockMove = GameObject.FindObjectOfType<Move>();
		grid = GameObject.FindObjectOfType<GridOverlay>();						    
    }

    #endregion

    public void BoardInitialize() 
	{				
		gameData.BoardInitialize();			

		mainCameraController.Prepare();		
		mainCameraController.PrepareCameraMoving();
		
		grid.Size = gameData.Size;
		grid.Draw ();								
		
		BordersCreate ();

        blockSpawner.HelpersCreate();        
        blockMove.PositionSetInitial(); 
		Collission.IsBlockCollision = false;               
	}	

	/// <sumary>
	/// Create Borders
	/// Crear BoxColliders para delimitar el tablero de juego y poder detectar cuando un bloque 
	/// se sale de los limites
	/// <sumary>
	private void BordersCreate() 
	{	
		GameObject border;
		float limit = 10f;

		Destroy (borderBottom);
		Destroy (borderLeft);
		Destroy (borderRight);
		Destroy (borderBehind);
		Destroy (borderFront);
		//Bottom
		border = (GameObject)Instantiate (Border);
		border.GetComponent<BoxCollider> ().center = new Vector3 (0, -limit / 2 - gameData.Step / 2, 0);
		border.GetComponent<BoxCollider> ().size = new Vector3 (limit, limit, limit);
		Tools.MoveToLayer(border.transform, LayerMask.NameToLayer("Default"));
		borderBottom = border;
		//Left
		border = (GameObject)Instantiate (Border);
		border.GetComponent<BoxCollider> ().center = new Vector3 (-limit / 2 - gameData.SizeOfBoard() / 2 - gameData.Step / 2, limit / 2, 0);
		border.GetComponent<BoxCollider> ().size = new Vector3 (limit, limit, limit);
		borderLeft = border;
		//Right
		border = (GameObject)Instantiate (Border);
		border.GetComponent<BoxCollider> ().center = new Vector3 (limit / 2 + gameData.SizeOfBoard() / 2 + gameData.Step / 2, limit / 2, 0);
		border.GetComponent<BoxCollider> ().size = new Vector3 (limit, limit, limit);
		borderRight = border;
		//Behind
		border = (GameObject)Instantiate (Border);
		border.GetComponent<BoxCollider> ().center = new Vector3 (0, limit / 2, limit / 2 + gameData.SizeOfBoard() / 2 + gameData.Step / 2);
		border.GetComponent<BoxCollider> ().size = new Vector3 (limit, limit, limit);
		borderBehind = border;
		//Front
		border = (GameObject)Instantiate (Border);
		border.GetComponent<BoxCollider> ().center = new Vector3 (0, limit / 2, -limit / 2 - gameData.SizeOfBoard() / 2 - gameData.Step / 2);
		border.GetComponent<BoxCollider> ().size = new Vector3 (limit, limit, limit);
		borderFront = border;
	}	    
}

}
