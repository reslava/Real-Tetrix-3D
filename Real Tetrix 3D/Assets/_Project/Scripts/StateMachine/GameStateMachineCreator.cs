using System;
using UnityEngine;

namespace RafaEslava {

public static class Conditions 
{	
	public static bool IsPlaying;
	public static bool IsSettings;
	public static bool IsGameStart;
	public static bool IsBlockHelperRotating;
	public static bool IsBlockDroping;
	public static bool IsPlanesChecking;
	public static bool IsPlaneDone;
	public static bool IsCameraMoving;
	public static bool IsGameOver;
	public static bool IsLevelUp;
	public static bool IsLifeLost;	
}

public class GameStateMachineCreator
{
    public StateMachine StateMachine { get; private set; }	
    
    public GameStateMachineCreator()
	{
		StateMachine = new StateMachine();

		var stateMenu = new StateMenu();
		var statePlaying = new StatePlaying();
		var stateBlockRotate = new StateBlockHelperRotating();
		var stateBlockDrop = new StateBlockHelperDropping();
		var statePlanesCheck = new StatePlanesChecking();
		var statePlaneDone = new StatePlaneDone();
		var stateCameraMove = new StateCameraMoving();
		var stateLevelUp = new StateLevelUp();
		var stateLifeLost = new StateLifeLost();
		var stateSettings = new StateSettings();
		var stateGameOver = new StateGameOver();								

		StateMachine.AddTransition(stateMenu, statePlaying, IsPlaying()); 
		StateMachine.AddTransition(statePlaying, stateMenu, IsPlayingExit());

		StateMachine.AddTransition(stateMenu, stateSettings, IsSettings());				
		StateMachine.AddTransition(statePlaying, stateSettings, IsSettings());		

		StateMachine.AddTransition(stateSettings, statePlaying, IsSettingsExitToPlay()); 		
		StateMachine.AddTransition(stateSettings, stateMenu, IsSettingsExitToMenu());				

		StateMachine.AddTransition(statePlaying, stateBlockRotate, IsBlockRotating());
		StateMachine.AddTransition(stateBlockRotate, statePlaying, IsBlockRotatingExit());
		StateMachine.AddTransition(statePlaying, stateBlockDrop, IsBlockDroping());
		StateMachine.AddTransition(stateBlockDrop, statePlanesCheck, IsPlanesChecking());
		StateMachine.AddTransition(statePlanesCheck, statePlaying, IsPlanesCheckingExit());
		StateMachine.AddTransition(statePlanesCheck, statePlaneDone, IsPlaneDone());		
		StateMachine.AddTransition(statePlaneDone, statePlanesCheck, IsPlaneDoneExit());
		StateMachine.AddTransition(statePlaying, stateCameraMove, IsCameraMoving());
		StateMachine.AddTransition(stateCameraMove, statePlaying, IsCameraMovingExit());
		StateMachine.AddTransition(statePlaying, stateLevelUp, IsLevelUp());		
		StateMachine.AddTransition(stateLevelUp, statePlaying, IsLevelUpExit());
		StateMachine.AddTransition(statePlaying, stateLifeLost, IsLifeLost());		
		StateMachine.AddTransition(stateLifeLost, statePlaying, IsLifeLostExit());
				
		StateMachine.AddTransition(statePlaying, stateGameOver, IsGameOver());		
		StateMachine.AddTransition(stateGameOver, stateMenu, IsGameOverExit());	

		StateMachine.SetInitialState(stateMenu);
		
		Func<bool> IsPlayingExit() => () => !Conditions.IsPlaying;
		Func<bool> IsPlaying() => () => Conditions.IsPlaying;
		Func<bool> IsBlockRotating() => () => Conditions.IsBlockHelperRotating;
		Func<bool> IsBlockRotatingExit() => () => !Conditions.IsBlockHelperRotating;
		Func<bool> IsBlockDroping() => () => Conditions.IsBlockDroping;
		Func<bool> IsPlanesChecking() => () => Conditions.IsPlanesChecking;
		Func<bool> IsPlanesCheckingExit() => () => !Conditions.IsPlanesChecking;
		Func<bool> IsPlaneDone() => () => Conditions.IsPlaneDone;
		Func<bool> IsPlaneDoneExit() => () => !Conditions.IsPlaneDone;
		Func<bool> IsCameraMoving() => () => Conditions.IsCameraMoving;
		Func<bool> IsCameraMovingExit() => () => !Conditions.IsCameraMoving;				
		Func<bool> IsLevelUp() => () => Conditions.IsLevelUp;
		Func<bool> IsLevelUpExit() => () => !Conditions.IsLevelUp;
		Func<bool> IsLifeLost() => () => Conditions.IsLifeLost;
		Func<bool> IsLifeLostExit() => () => !Conditions.IsLifeLost;
		Func<bool> IsSettings() => () => Conditions.IsSettings;
		Func<bool> IsSettingsExitToPlay() => () => !Conditions.IsSettings && Conditions.IsPlaying;
		Func<bool> IsSettingsExitToMenu() => () => !Conditions.IsSettings && !Conditions.IsPlaying;
		Func<bool> IsGameOver() => () => Conditions.IsGameOver;
		Func<bool> IsGameOverExit() => () => !Conditions.IsGameOver;		
	}
}

}
