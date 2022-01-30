using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

/// <sumary> 
/// OBJETIVE: Manage (create/destroy) any kind of block/cube 
/// <sumary>
public class Spawner : MonoBehaviour
{        		
	public GameData gameData;
	
	private Planes planesManager;
	private Pieces blocks;
	private Move blockMove;
	private Drop blockDrop;

	private Piece block;	

	private void OnEnable() 
	{
		Events.OnLevelUpEnter.AddListener(OnLevelShift);	
		Events.OnLifeLostEnter.AddListener(OnLevelShift);			
	}

	private void OnDisable() 
	{
		Events.OnLevelUpEnter.RemoveListener(OnLevelShift);	
		Events.OnLifeLostEnter.RemoveListener(OnLevelShift);			
	}

    private void Awake() 
    {       				
		planesManager = GameObject.FindObjectOfType<Planes>();
		blocks = GameObject.FindObjectOfType<Pieces>();
		blockMove = GameObject.FindObjectOfType<Move>();
		blockDrop = GameObject.FindObjectOfType<Drop>();
    }		

	public void BlockHelperCreate(int iBlock = 0)
	{		
		if (blocks.Player != null)
			Destroy(blocks.Player);											
		
		Piece piece = gameData.GetPiece();		
				
		piece = Instantiate(piece, blockMove.PositionNext, Quaternion.identity);		
		//blocks.Player = (GameObject)Instantiate(gameData.CurrentLevel.Blocks[gameData.iBlock - 1], blockMove.PositionNext, Quaternion.identity);		
		piece.GetComponent<States>().State = BlockState.Helper;						
		piece.transform.parent = blocks.PlayerParent.transform;	
		
		blocks.Player = piece.gameObject;

		blocks.PlayerParent.GetComponent<Move>().BlockHelper = piece.gameObject;
		blocks.PlayerParent.GetComponent<Rotate>().BlockHelper = piece.gameObject;
		blocks.PlayerParent.GetComponent<Drop>().BlockHelper = piece.gameObject;

		blockDrop.OnStep = true;

		blockMove.PositionNext.y = planesManager.PlaneGetY(planesManager.PlaneGetFirstEmpty());
		Collission.IsBlockCollision = false;
		
		
		BlockHelperNextCreate();		
	}	
	
	public void BlockHelperNextCreate(int indexBlockNext = 0)
	{				
		if (blocks.Next != null)
			Destroy(blocks.Next);
			
		Piece pieceNext = gameData.GetPieceNext();		
		
		pieceNext = Instantiate(pieceNext);		
		//blocks.Next = (GameObject)Instantiate(gameData.CurrentLevel.Blocks[gameData.iBlockNext - 1]);		
		pieceNext.GetComponent<States>().State = BlockState.HelperNext;													
		pieceNext.transform.parent = blocks.NextParent.transform;

		blocks.Next = pieceNext.gameObject;
	}        	

	/// <sumary>	
	/// If no BlockCollision
	///   Create a copy of BlockHelper as a BlockNormal and put it under BlocksDropped gameobject. Destroy the old Helper
	/// <sumary>
	public void BlockCreate() 
	{
		if (!Collission.IsBlockCollision && Conditions.IsBlockDroping) 
		{										
			//block = (GameObject)Instantiate (gameData.CurrentLevel.Blocks[gameData.iBlock - 1], blocks.Player.transform.position, Quaternion.identity);						
			block = Instantiate (gameData.PiecePrefab, blocks.Player.transform.position, Quaternion.identity);						
			block.GetComponent<States>().Copy(blocks.Player);
			block.GetComponent<States>().State = BlockState.Normal;
			block.transform.parent = blocks.DroppedParent.transform;			

			Destroy(blocks.Player);

			blockMove.PositionNext.y = planesManager.PlaneGetY(planesManager.PlaneGetFirstEmpty());
		}
	}
	
	/// <sumary>
	/// Player input choose to Drop the Block
	/// If no BlockCollision
	///   Create a copy of BlockHelper as a BlockNormal and put it under BlocksDropped gameobject
	/// <sumary>
	public void BlockDropInput()
	{
		if (!Collission.IsBlockCollision) 						
			Conditions.IsBlockDroping = true;		
	}	

	public void CubesDroppedAdd()
	{				

		//blocks.Add(block);
		while( block.transform.childCount > 0)		
			block.transform.GetChild(0).parent = blocks.DroppedParent.transform;
		
		Destroy(block.gameObject);
		
	}

	public void CubesDroppedDestroyAll() 
	{
		foreach(Transform transform in blocks.DroppedParent.transform)		
			Destroy(transform.gameObject);
	}

	public void OnLevelShift()
	{
		HelpersCreate();
		blockMove.PositionSetInitial();
	}
	public void HelpersCreateDefault()
	{
		HelpersCreate(0);
	}
	
	public void HelpersCreate(int iBlock = 0)
	{
		if(iBlock == 0) CubesDroppedDestroyAll ();
		Collission.IsBlockCollision = false;
		BlockHelperCreate (iBlock);	
		if(iBlock != 0) blockMove.MoveToPositionNext();
		Collission.IsBlockCollision = false;							
	}	
}

}