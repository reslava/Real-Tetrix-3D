using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RafaEslava
{

public class UIController : MonoBehaviour
{    
    public GameData gameData;    
    public GameDataRecords gameDataRecords; 
    [SerializeField] private GameInitializer gameController;

    private Spawner blocksManager;
    private Board board;

    [Header("Canvas")]    
    [SerializeField] private Canvas CanvasMenu; 
    [SerializeField] private Canvas CanvasPlay;	
    [SerializeField] private Canvas CanvasAdsInterstitial;     
    [SerializeField] private Canvas CanvasSettings;
    [SerializeField] private Canvas CanvasLifeLost;
    [SerializeField] private Canvas CanvasGameOver;
    [SerializeField] private Canvas CanvasLevelUp; 

    [Header("Settings")]    
	[SerializeField] private Button ButtonPlaySettings;
    [SerializeField] private Button ButtonLoad;			    

    [Header("LifeLost Lost")]    
    [SerializeField] private GameObject Heart1;
    [SerializeField] private GameObject Heart2;
    [SerializeField] private GameObject Heart3;    
    [SerializeField] private TextMeshProUGUI textLifeLost;    
    
    [Header("Game Over")]    
    [SerializeField] private TextMeshProUGUI TextScoreGameOver;    	

    [Header("Menu / Level Up")]       
    [SerializeField] private GameObject MenuGroup;  
    [SerializeField] private GameObject LevelUpGroup;  

    [Header("Help")]       
    [SerializeField] private GameObject PanelHelp; 
    


    private bool isBackground = false;  

    #region MONOBEHAVIOURS EVENTS *******************************************************

    private void OnEnable() 
	{        
        // State Menu
		Events.OnMenuEnter.AddListener(OnMenuEnter);                                       

        Events.OnLevelUpEnter.AddListener(OnLevelUpEnter);			        
        Events.OnLifeLostEnter.AddListener(OnLifeLostEnter);
        Events.OnGameOverEnter.AddListener(OnGameOverEnter);               	    
	}

	private void OnDisable() 
	{
        // State Menu
		Events.OnMenuEnter.RemoveListener(OnMenuEnter);             

        Events.OnLevelUpEnter.RemoveListener(OnLevelUpEnter);        
        Events.OnLifeLostEnter.RemoveListener(OnLifeLostEnter);
        Events.OnGameOverEnter.RemoveListener(OnGameOverEnter);                   
	}   

    private void Awake() 
    {               
        blocksManager = GameObject.FindObjectOfType<Spawner>();    
        board = GameObject.FindObjectOfType<Board>();
    }  

    private void Start()     
    {    
        Initialize();
    }

    #endregion

    private void Initialize()
    {
        //TextScoreFlash.enabled = false;
		OnToogleBackgroundValueChanged (true);
    }    

    
    public void OnToogleBackgroundValueChanged(bool value) 
    {
		isBackground = !isBackground;

		GameObject.Find ("Table").GetComponent<MeshRenderer>().enabled = isBackground;		
		GameObject.Find ("Plane").GetComponent<MeshRenderer>().enabled = !isBackground;		
	}	
 
    public void OnMenuEnter()
    {        
        Conditions.IsPlaying = false;
        Conditions.IsSettings = false;
        CanvasGameObjectsDisable();
        CanvasMenu.gameObject.SetActive (true);
        MenuGroup.SetActive(true);
        LevelUpGroup.SetActive(false);
        board.BoardInitialize ();	                
		//Board.Instance.BoardInitialize ();	                
        // // blocksManager.HelpersCreate();
        
        // // BlockHelperController.Instance.PositionSetInitial();                
        ButtonLoad.interactable = GameDataManager.Instance.IsSaved();        
    }


    public void OnPlay()    
    {   
        Tools.IsPaused = false;
        Conditions.IsPlaying = true;        
        CanvasGameObjectsDisable();        		
        CanvasPlay.gameObject.SetActive (true);	
        Conditions.IsLevelUp = false;  	        
    }   

    public void OnLevelUpEnter()
    {                        
        CanvasGameObjectsDisable();
        CanvasMenu.gameObject.SetActive (true);    
        MenuGroup.SetActive(false);
        LevelUpGroup.SetActive(true);
        board.BoardInitialize ();	 
        //TextLevelUp.text = "New level " + gameData.Level;   
    }       
    
    public void OnLevelUpExit()
    {                        
        CanvasGameObjectsDisable();
        CanvasPlay.gameObject.SetActive (true);    
        MenuGroup.SetActive(true);
        LevelUpGroup.SetActive(false);
        Conditions.IsLevelUp = false;        
    }

    public void OnSettings()
    {                
        CanvasGameObjectsDisable();
        CanvasSettings.gameObject.SetActive (true);        

        Conditions.IsSettings = true;
        //Conditions.IsPlaying = true;
    }

    public void OnSettingsExitToPlay()
    {                
        CanvasGameObjectsDisable();
        CanvasPlay.gameObject.SetActive (true);        

        Conditions.IsSettings = false;
        Conditions.IsPlaying = true;
    }

    public void OnSettingsExitToMenu()
    {                
        CanvasGameObjectsDisable();
        CanvasMenu.gameObject.SetActive (true);        

        Conditions.IsSettings = false;
        Conditions.IsPlaying = false;
    }

    public void OnLevelUpFinishedShowAdsInterstitial()
	{
        // if(UnityEngine.Random.value >= 0.5f)
        // {
        //     CanvasGameObjectsDisable();
        //     CanvasAdsInterstitial.gameObject.SetActive (true);     
        // }

		Conditions.IsLevelUp = false;        
	}
    
    public void OnGameOverEnter()
    {        
        CanvasGameObjectsDisable();
        CanvasGameOver.gameObject.SetActive (true);     
        
        //GameDataRecords gameDataRecords = gameData.GetGameDataRecords();
        long scoreHigh = gameDataRecords.ScoreHigh;
        int levelHigh = gameDataRecords.LevelHigh;
        string newScoreHigh = "";
        string newLevelHigh = "";
        bool newRecord = false;
        if(gameData.Score > scoreHigh)
        {
            gameDataRecords.ScoreHigh = gameData.Score;
            newScoreHigh = " ** New score record **";
            newRecord = true;
        }
        if(gameData.Level > levelHigh)
        {
            gameDataRecords.LevelHigh = gameData.Level;
            newLevelHigh = " ** New max level **";
            newRecord = true;
        }
        TextScoreGameOver.text = "Score... " + string.Format("{0:N0}", gameData.Score) + newScoreHigh + "\n" +
                                 "Level... " + string.Format("{0:N0}", gameData.Level) + newLevelHigh + "\n" +
                                 "High Score... " + string.Format("{0:N0}", scoreHigh) + "\n" +
                                 "Max Level... " + string.Format("{0:N0}", levelHigh) ;   
        if (newRecord)
            GameDataRecordsManager.Instance.WriteFile();                
    }

    public void OnGameOverShowAdsInterstitial()
    {
        CanvasGameObjectsDisable();
        
        // CanvasAdsInterstitial.gameObject.SetActive (true);     
        
        Conditions.IsGameOver = false;        
    }   

    public void OnHelp() 
	{
        Tools.IsPaused = true;		        
		PanelHelp.SetActive (true);        
	}	

    public void OnHelpFinished() 
	{
        Tools.IsPaused = false;		        
		PanelHelp.SetActive (false);        
	}

    public void OnLifeLostEnter() 
	{        	
        CanvasGameObjectsDisable();
		CanvasLifeLost.gameObject.SetActive (true);        

        int lives = gameData.Lives;
        textLifeLost.text = lives + " lives left";
        if(lives < 3)
        {
            Heart3.SetActive(false);
            if(lives < 2)
            {
                textLifeLost.text = "1 life left!!!";
                Heart2.SetActive(false);
            }
        }
	}    

    public void OnLifeLostExit()
    {                        
        CanvasGameObjectsDisable();
        CanvasPlay.gameObject.SetActive (true);    
        Conditions.IsLifeLost = false;        
    }

    private void CanvasGameObjectsDisable() 
    {
        foreach(Transform transform in gameObject.transform)
            transform.GetComponent<Canvas>().gameObject.SetActive(false);		
    }
}

}