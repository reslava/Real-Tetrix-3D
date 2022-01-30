using UnityEngine;

namespace RafaEslava {

public class GameInitializer : MonoBehaviour
{				
	public StateMachine GameStateMachine { get; private set; }				
	
	public GameData gameData;			    							

	private void Awake() 
	{														      		
		GameStateMachine = new GameStateMachineCreator().StateMachine;
	}

	private void Start()
	{							
		Initialize();				
		
		GameDataRecordsManager.Instance.ReadFile();	

		GameStateMachine.Start();		
	}

	private void Update() => GameStateMachine.Tick();	

	private void Initialize()
	{
		Conditions.IsPlaying = false;
        Conditions.IsGameOver = false;
        Tools.IsPaused = false; 

		gameData.Initialize();		
	}		
	
	public void OnPlay()
	{
		Conditions.IsPlaying = true;
	}
}

}