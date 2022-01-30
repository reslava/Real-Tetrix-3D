using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

[System.Serializable]
public struct CubeDropped
{
    public string Id;
    public float x, y, z; // transform.position
}

[CreateAssetMenu(fileName="GameData" ,menuName="Tetrix/GameData")]
public class GameData : ScriptableObject
{       
    public GameDataRecords gameDataRecords;

    // Data for actual game state
    [Header("*** Game Score")]
    public int Lives;
    public long Score;
    public int ScoreNew;  
    public long ScoreCurrentLevel;       
    public int planesDone;	    
    public int planesDoneAtOnce;

    [Header("*** Score")]	    
    public int Level;      
    public int Blocks;
    public int BlocksPoints;
    public int Coins;
    public int CoinsPoints;
    public int PlaneDoneAtOnce;
    public int planesExtra;

    [Header("*** Levels")]	    
    public LevelData[] LevelsData;

    [Header("*** Cubes")]	    
    [SerializeField] public Cube[] CubesAvailables = new Cube[0];            
    public Dictionary<string, Cube> IdToCubesAvailables { get; private set; }    
    
    [Header("*** Game Level")]    
    public int PlanesToDo; 
    public int Size { get => CurrentLevel.Size; }  // Size of grid-board          

    // Helpers getters
    public int NumberOfLevels { get => LevelsData.Length; }
    public LevelData CurrentLevel { get => LevelsData[Level - 1]; }
    // public int NumberOfBlocks { get => CurrentLevel.Pieces.Length; }     
    
    public int LevelUpMaxTimeTo { get => CurrentLevel.LevelUpMaxTimeTo; }
    public float levelUpTimer;   // Time since level started
    public int LevelUpTimeTo;    // max - timer
    
    public int PlaneMaxTimeTo { get => CurrentLevel.PlaneMaxTimeTo; }	       
    public float planeTimer;     // Time since last plane done    
    public int PlaneTimeTo;	     // max - timer     

    [Header("*** Blocks")]
    public Piece PiecePrefab;
    public Piece PiecePrefabNext;
    public int iBlock;
    public int iBlockNext; // Block Helper & Helper Next indexs
    public Vector3 BlockHelperPosition;
    [SerializeField] public List<CubeDropped> CubesDroppedList;        

    [Header("*** Settings")]
    #if DEBUG_GAME_ON
        [System.NonSerialized] public readonly int LevelMaxDefault = 16; 
    #else
        [System.NonSerialized] public readonly int LevelMaxDefault = 3; // Max level when players start to play the game, if he do LevelHigh record above he can play that
    #endif
    [System.NonSerialized] public readonly int maxYStart = 15; // Max high level to create a new block helper       
    //private int levelMax = 15; // Level (related to Grid Size and....future)
    //private int difficulty = 1; // 1 2 3   

    // Depending on grid size even or not 
    public float xEven {get; private set;}   // x reference for blocks
	public int xMax {get; private set;}      // x max for blocks, used in planes raycasting  

    //[SerializeField] private bool withDifficultBlocks;

    // Si las pongos como constantes no se como acceder a ellas desde las otras clases
	[System.NonSerialized] public readonly float Step = 0.2f;
	[System.NonSerialized] public readonly float Gap = 0.01f;    
    
    // Settings	

    private void IdToCubesAvailablesInitialize() 
    {
        IdToCubesAvailables = new Dictionary<string, Cube>();
        foreach(var cube in CubesAvailables)
        {
            IdToCubesAvailables.Add(cube.Id, cube);
        }
    }

    public Piece GetPiece()
    {
        if(PiecePrefab == null)
            PiecePrefab = CurrentLevel.RandomPiece();
        else
            PiecePrefab = PiecePrefabNext;

        return PiecePrefab;                  
    }

    public Piece GetPieceNext()    
    {        
        // if(PieceNext != null)
        //     Destroy(PieceNext);

        PiecePrefabNext = CurrentLevel.RandomPiece();        

        return PiecePrefabNext;                  
    }
	
    public void NewScoreInitialize()
    {        
        Blocks = 0;
        BlocksPoints = 0;
        Coins = 0;       
        CoinsPoints = 0;
    }
    
    public void Initialize() 
    {    
        Lives = 3;                        
        
        Level = 1; 
        try
        {
            if(gameDataRecords.LevelHigh > 3) 
                Level = gameDataRecords.LevelHigh;
        }
        catch { Level = 1; }
        
        Score = 0;        
        planesDone = 0;  
        planesExtra = 0;                  
        
    }
    
    public float SizeOfBoard() => Size * Step + Gap;

    //private void Start() 
    private GameData()
    {        
        Initialize();
    }

    private void OnEnable() 
	{
        IdToCubesAvailablesInitialize();
        // State Menu
        Events.OnMenuEnter.AddListener(Initialize);        
        
        // State Playing
        Events.OnPlayingTick.AddListener(OnPlayingTick);			        


        Events.OnBlockHelperDroppingExit.AddListener(OnBlockDroppingExit);

        // Ads
        Events.OnAdsReward.AddListener(OnAdsReward);
        //Events.OnPlaneDone.AddListener(OnPlaneDone);		        
	}

	private void OnDisable() 
	{
        // State Menu
        Events.OnMenuEnter.RemoveListener(Initialize);        

        // State Playing
		Events.OnPlayingTick.RemoveListener(OnPlayingTick);	                 

        Events.OnBlockHelperDroppingExit.RemoveListener(OnBlockDroppingExit);

        // Ads
        Events.OnAdsReward.RemoveListener(OnAdsReward);		        
        //Events.OnPlaneDone.RemoveListener(OnPlaneDone);
	} 

    public void OnPlaneDone()
    {
        planesDone++;
        planesDoneAtOnce++;
        if(planesDoneAtOnce > 1)
            Events.OnPlanesDoneAtOnce?.Invoke(planesDoneAtOnce);        
        PlanesToDo--;
        if(PlanesToDo == 0) 
        {                        
            //Events.OnLevelUpReached?.Invoke();        
            if(gameDataRecords.LevelHigh < Level + 1)
            {
                gameDataRecords.LevelHigh++;
                Events.OnLevelHighRecord?.Invoke(gameDataRecords.LevelHigh);        
            }
        }        
        planeTimer = 0f;        
        
        ScoreNew = CurrentLevel.LevelScoreMultiplier * (planesDoneAtOnce*planesDoneAtOnce) * (CoinsPoints + BlocksPoints); 
        planesExtra = 0;

        if(PlanesToDo < 0)
        {   
            planesExtra = - PlanesToDo;
            ScoreNew = ScoreNew * planesExtra*planesExtra;
        }
		Score += ScoreNew;                 
        ScoreCurrentLevel += ScoreNew;   
        
        

        if(Score > gameDataRecords.ScoreHigh)           
        {
            gameDataRecords.ScoreHigh = Score;
            Events.OnScoreRecord?.Invoke(); 
        }
        if(ScoreCurrentLevel > gameDataRecords.ScoresHighByLevel[Level - 1])           
        {
            gameDataRecords.ScoresHighByLevel[Level - 1] = ScoreCurrentLevel;
            Events.OnScoreByLevelRecord?.Invoke(Level); 
        }                          

        Events.OnSetScore?.Invoke (Score);
        Events.OnPlaneDone?.Invoke();
    }
    public void OnAdsReward()
    {
        Lives++;        
    }

    private void OnBlockDroppingExit()
    {
        planesDoneAtOnce = 0;
    }            

    public void OnPlayingTick() 
    {        
        levelUpTimer += Time.deltaTime;
        planeTimer += Time.deltaTime;
        LevelUpTimeTo = LevelUpMaxTimeTo - ((int)levelUpTimer);
        PlaneTimeTo = PlaneMaxTimeTo - ((int)planeTimer);         
        
        if(LevelUpTimeTo <= 0 || PlaneTimeTo <= 0) 
        {
            if(PlanesToDo <= 0)
            {                     
                Level++;            
                Events.OnLevelUpReached?.Invoke(Level);  
                LevelInitialize();                                      		                        

                Conditions.IsLevelUp = true;                    
            }
            else
            {
                Lives--;
                if(Lives == 0)
                    Conditions.IsGameOver = true;                
                else
                {                
                    Conditions.IsLifeLost = true;                                                          
                    LevelInitialize();
                }    
            }            
        }        
    }			

    public void OnPlay()
    {
        Debug.Log("ONNNNNNNNN PLAYYYYYYYYYYYY");
        
        LevelInitialize();        
    }            

    public void OnLevelUpEnter()
    {
        Conditions.IsLevelUp = true;                    

        Level++;
        
        LevelInitialize();                                      		                                         
    }

    public void LevelInitialize() 
    {        
        Debug.Log("ONNNNNNNNN LEVELLLLLLLLLLL UPPPPPPPPPPPPPPPPPPPPP");        
        planesDone = 0;     

        levelUpTimer = 0f;
        planeTimer = 0f;
        ScoreCurrentLevel = 0;

        PlanesToDo = CurrentLevel.PlanesToDo;
        
        Events.OnSetScore.Invoke(Score);
    }                     

    public void BoardInitialize()
    {      		
		if (Size % 2 == 0) 
        {
			xEven = Step / 2;
			xMax = 1;
			
		} else {
			xEven = 0f;
			xMax = 0;
		}		
    }        
}

}
