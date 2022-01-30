using System;
namespace RafaEslava
{
    public static class Events
    {   
        // State Menu        
        public static readonly EventSimple OnMenuEnter2 = new EventSimple();
        
        public static readonly Event OnMenuEnter = new Event();
        public static readonly Event OnGameStart = new Event();

        // State Playing
        public static readonly Event OnPlayingTick = new Event();

        // State BlockHelperDropping 
        public static readonly Event OnBlockHelperDroppingTick = new Event();
        public static readonly Event OnBlockHelperDroppingExit = new Event();

        // State BlockHelperRotating
        public static readonly Event OnBlockHelperRotatingTick = new Event();
        public static readonly Event OnBlockHelperRotatingEnter = new Event();

        // State PlaneDone
        public static readonly Event OnPlaneDoneTick = new Event();
        public static readonly Event OnPlaneDoneEnter = new Event();
        public static readonly Event OnPlaneDoneExit = new Event();        

        // State LevelUp
        public static readonly Event OnLevelUpEnter = new Event();

        // State LifeLost
        public static readonly Event OnLifeLostEnter = new Event();        

        // State Settings
        public static readonly Event OnSettingsEnter = new Event();        

        // State GameOver
        public static readonly Event OnGameOverEnter = new Event();        

        // BlockHelperController        
        public static readonly Event OnBlockHelperMove = new Event();        
        public static readonly Event OnBlockHelperOnStep = new Event();        
        
        // GameData 
        public static readonly Event OnNewScore = new Event();
        public static readonly Event<int> OnPlanesDoneAtOnce = new Event<int>();
        public static readonly Event<int> OnLevelUpReached = new Event<int>();
        // Score 
        // TODO: gameData se tiene que acceder desde UIController (y desde donde haga falta) por lo que pasar el parametro Score carece de sentido: eliminar parametro
        public static readonly Event<long> OnSetScore = new Event<long>();


        // Tools.IsPaused Banner uses to show & hide
        public static readonly Event OnPauseEnter = new Event();
        public static readonly Event OnPauseExit = new Event();


        // PlaneManager PlaneClean
        public static readonly Event OnPlaneDoneAndCleaned = new Event();        
        public static readonly Event OnPlaneDone = new Event();   

        //public static readonly Event OnDropped = new Event();   

        // Ads               
        public static readonly Event<bool> OnInterstitialLoaded = new Event<bool>();
        public static readonly Event<bool> OnRewardedLoaded = new Event<bool>();
        public static readonly Event OnInterstitialShowComplete = new Event();
        public static readonly Event OnAdsReward = new Event();      

        // GPS / Records
        public static readonly Event<int> OnLevelHighRecord = new Event<int>();      
        public static readonly Event OnScoreRecord = new Event();      
        public static readonly Event<int> OnScoreByLevelRecord = new Event<int>();                 
    }
}

    // Events.___?.Invoke();	

    // private void OnEnable() 
	// {
	// 	Events.On___.AddListener(___);			
	// }

	// private void OnDisable() 
	// {
	// 	Events.On___.RemoveListener(___);			
	// }