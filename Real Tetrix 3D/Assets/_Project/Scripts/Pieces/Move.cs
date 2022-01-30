using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {
    
public class Move : MonoBehaviour
{
    private MainCameraController mainCameraController;
	private Planes planesManager;
	public GameData gameData;	

    public GameObject BlockHelper;
	public Vector3 Position 
	{ 
		get 
		{
			if(BlockHelper != null)
				return BlockHelper.transform.position;
			return new Vector3(0, 0, 0);
		}
		set => BlockHelper.transform.position = value;
	}
	public Vector3 PositionNext;

    private const float Step = 0.2f;
    private const float Gap = 0.01f;    

    public void PositionSetInitial()
	{
		// Set Position next after create to keep Y = Step / 2
		float x, y, z;
		y = gameData.Step / 2;
		if (gameData.Size % 2 == 0)
			x = z = gameData.Step / 2;
		else
			x = z = 0; 				
		
		Position = PositionNext = new Vector3(x, y, z);				
	}    	

    private void Awake()      
	{	                        
		mainCameraController = GameObject.FindObjectOfType<MainCameraController>();		
		planesManager = GameObject.FindObjectOfType<Planes>();						
    }	

    //******************************************************************************************
	// MoveLeft, MoveRight, MoveUp and MoveDown x and z coordinates depending of camera position
	//******************************************************************************************
	public void MoveLeft()//ref float x, ref float z) 
    {
        //!TODOEvents.
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().clip = Block_Move;
		//!audblockHelper.GetComponent<AudioSource> ().Play();
		switch (mainCameraController.CameraPosition) 
        {		
            case MainCameraController.CameraPositionsEnum.front:                
				PositionNext.x = Position.x - Step;
                break;
            case MainCameraController.CameraPositionsEnum.behind:                
				PositionNext.x = Position.x + Step;
                break;
            case MainCameraController.CameraPositionsEnum.left:                
				PositionNext.z = Position.z + Step;
                break;
            case MainCameraController.CameraPositionsEnum.right:                
				PositionNext.z = Position.z - Step;
                break;
		}
	}
	public void MoveRight() 
	{
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().clip = Block_Move;
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().Play();
		switch (mainCameraController.CameraPosition) 
        {		
            case MainCameraController.CameraPositionsEnum.front:                
				PositionNext.x = Position.x + Step;
                break;
            case MainCameraController.CameraPositionsEnum.behind:                
				PositionNext.x = Position.x - Step;
                break;
            case MainCameraController.CameraPositionsEnum.left:                
				PositionNext.z = Position.z - Step;
                break;
            case MainCameraController.CameraPositionsEnum.right:                
				PositionNext.z = Position.z + Step;
                break;
		}
	}
	public void MoveUp()//ref float x, ref float z) 
	{
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().clip = Block_Move;
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().Play();
		switch (mainCameraController.CameraPosition) 
        {		
            case MainCameraController.CameraPositionsEnum.front:                
				PositionNext.z = Position.z + Step;
                break;
            case MainCameraController.CameraPositionsEnum.behind:                
				PositionNext.z = Position.z - Step;
                break;
            case MainCameraController.CameraPositionsEnum.left:                
				PositionNext.x = Position.x + Step;
                break;
            case MainCameraController.CameraPositionsEnum.right:                
				PositionNext.x = Position.x - Step;
                break;
		}
	}
	public void MoveDown()//ref float x, ref float z) 
	{
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().clip = Block_Move;
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().Play();
		switch (mainCameraController.CameraPosition) 
        {		
            case MainCameraController.CameraPositionsEnum.front:                
				PositionNext.z = Position.z - Step;
                break;
            case MainCameraController.CameraPositionsEnum.behind:                
				PositionNext.z = Position.z + Step;
                break;
            case MainCameraController.CameraPositionsEnum.left:                
				PositionNext.x = Position.x - Step;
                break;
            case MainCameraController.CameraPositionsEnum.right:                
				PositionNext.x = Position.x + Step;
                break;
		}
	}

	public void MoveYDown() 
	{
		if (PositionNext.y > Step)
			PositionNext.y -= Step;
	}

	public void MoveYUp() 
	{
		if(PositionNext.y < planesManager.PlaneGetY(gameData.maxYStart))
			PositionNext.y += Step;
	}

	public void MoveToPositionNext()
	{
		if(Position != PositionNext)
		{
			Position = PositionNext;
			Events.OnBlockHelperMove?.Invoke();
		}
	}
    }
}
