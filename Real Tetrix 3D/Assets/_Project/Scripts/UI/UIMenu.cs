using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RafaEslava {

public class UIMenu : Singleton<UIMenu>
{    
    public GameData gameData;    
    public GameDataRecords gameDataRecords; 

    [Header("Menu")]    	   
    [SerializeField] private Button ButtonLevelUp;
    [SerializeField] private Button ButtonLevelDown;
    [SerializeField] private TextMeshProUGUI TextMenuLevel;       	
    [SerializeField] private TextMeshProUGUI TextNotAvailable;       	
    [SerializeField] private Image ImageLock;       	
    [SerializeField] private Button ButtonPlay;
    [SerializeField] private Button ButtonSettings;    

    [Header("*** Pieces availables")]	    
    [SerializeField] private Piece[] PiecesAvailables = new Piece[0];            
    private Dictionary<string, Piece> _idToPiecesAvailables;

    [SerializeField] private GameObject _cameraLevelPieces;
    [SerializeField] private TextMeshProUGUI TextLevelName;    
    [SerializeField] private TextMeshProUGUI TextLevelData;    
    [SerializeField] private TextMeshProUGUI TextLevelDescription;   

    [Header("*** Board")]	    
    [SerializeField] private GameObject _table;   
    [SerializeField] private GameObject _plane;   

    #region MONOBEHAVIOURS EVENTS *******************************************************

    private void IdToPiecesAvailablesInitialize() 
    {
        _idToPiecesAvailables = new Dictionary<string, Piece>();
        foreach(Piece piece in PiecesAvailables)
        {
            _idToPiecesAvailables.Add(piece.Id, piece);
        }
    }

    private void Awake() 
    {
        IdToPiecesAvailablesInitialize();    
    }

    private void OnEnable()     
    {      
        // State Menu
		Events.OnMenuEnter.AddListener(OnMenuLevel);                 
        Events.OnLevelUpEnter.AddListener(OnMenuLevel);           
	}

	private void OnDisable() 
	{
        // State Menu
		Events.OnMenuEnter.RemoveListener(OnMenuLevel);
        Events.OnLevelUpEnter.RemoveListener(OnMenuLevel);                                  
	}         

    #endregion                                             
   
    public void OnMenuLevel() 
    {	         
        int level = gameData.Level;
        Debug.Log(level + ".....................................COQUINA");

        TextMenuLevel.text = level.ToString();       
        int levelMax = gameDataRecords.LevelHigh> gameData.LevelMaxDefault?  gameDataRecords.LevelHigh: gameData.LevelMaxDefault;
            
        if(level > levelMax)
		{
			TextNotAvailable.enabled = true;
            ImageLock.enabled = true;            
			ButtonPlay.interactable = false;            
            ButtonSettings.interactable = false;			
		}
		else
		{
			TextNotAvailable.enabled = false;
            ImageLock.enabled = false;
			ButtonPlay.interactable = true;			            
            ButtonSettings.interactable = true;			
		}
        if(level > 16)
        {
            _table.transform.localScale = new Vector3(2, 2, 1);
            _plane.transform.localScale = new Vector3(2, 2, 1);
        }            
        else
        {
            _table.transform.localScale = new Vector3(1, 1, 1);
            _plane.transform.localScale = new Vector3(1, 1, 1);   
        }

        LevelData levelData = gameData.LevelsData[level - 1];
        TextLevelName.text = levelData.Name;
        string levelUpMaxTimeTo = string.Format("{0:0}:{1:00}", gameData.LevelUpMaxTimeTo / 60, gameData.LevelUpMaxTimeTo % 60);
        string planeMaxTimeTo = string.Format("{0:0}:{1:00}", gameData.PlaneMaxTimeTo / 60, gameData.PlaneMaxTimeTo % 60);
        TextLevelData.text = $"Fill {gameData.PlanesToDo.ToString()}  planes\n{levelUpMaxTimeTo} mins\n{planeMaxTimeTo} mins";        
        TextLevelDescription.text = levelData.Description.Replace("\\n","\n");        
        
        ButtonLevelDown.gameObject.SetActive(level > 1);
        ButtonLevelUp.gameObject.SetActive(level <= gameData.LevelsData.Length - 1);

        foreach(Transform transform in _cameraLevelPieces.transform)        
            transform.gameObject.SetActive(false);
        
        foreach(PieceAvailable pieceAvailable in gameData.CurrentLevel.PiecesAvailables)
        {
            if(_idToPiecesAvailables.TryGetValue(pieceAvailable.Piece.Id, out Piece piece))
            {
                piece.gameObject.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("UI Menu error");
            }
        }
        // var levelBlocksAvailables = levelData.Pieces.Length;
        // Debug.Log("Tetrix PREFABS *** " +Blocks[0].name);

        // // Para transparencia hay que poner rendering mode en Transparent, pero entonces los materiale no reciben sombras
        // // Lo dejamos en Opaque y utilizamos SetActive
        // // Color c;
        
        // for(var i = 0; i < levelBlocksAvailables; i++)        
        //     //Blocks[i].GetComponent<BlockSetup>().RestoreColor();
        //     Blocks[i].SetActive(true);
        // for(var i = levelBlocksAvailables; i < Blocks.Length; i++)
        // {
        //     //c = Blocks[i].GetComponent<BlockSetup>().Color;
        //     //c.a = 0.30f;                        
        //     //Blocks[i].GetComponent<BlockSetup>().SetColor(c);
        //     Blocks[i].SetActive(false);
        // }		
	}  
}

}