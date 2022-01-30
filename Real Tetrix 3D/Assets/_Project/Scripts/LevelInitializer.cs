using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class LevelInitializer : MonoBehaviour
{
	public GameData gameData;	

    private Board board; 
    private Move blockMove;        

	private void OnEnable() 
	{
		Events.OnMenuEnter.AddListener(LevelInitialize);				
	}	

	private void OnDisable() 
	{
		Events.OnMenuEnter.RemoveListener(LevelInitialize);				
	}	

	private void Awake() 
	{				
		board = GetComponent<Board>();		
		blockMove = GameObject.FindObjectOfType<Move>();									      			
	}        

    public void LevelInitialize()
	{		
		gameData.LevelInitialize();
		board.BoardInitialize ();
		blockMove.PositionSetInitial();	
	}   

    public void OnMenuLevelUp() 
	{	
		if(gameData.Level < gameData.LevelsData.Length)	
		{
			gameData.Level++;
			LevelInitialize();
		}
	}

	public void OnMenuLeveDown() 
	{	
		if(gameData.Level > 1)	
		{
			gameData.Level--;
			LevelInitialize();
		}
	}
}

}