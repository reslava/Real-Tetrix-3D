using System;
using UnityEngine;
using TMPro;

namespace RafaEslava {

public class UiPlay : MonoBehaviour
{
    public GameData gameData;    

    [Header("Play: Controls")]
    [SerializeField] private Canvas CanvasPlay;	
    [SerializeField] private GameObject CameraControls;    
    [SerializeField] private GameObject Explosion;	    

    [Header("Play: Game Data")]
    [SerializeField] private TextMeshProUGUI textScore;	
    [SerializeField] private TextMeshProUGUI textPlanesToDo;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textLives;

    [SerializeField] private TextMeshProUGUI textLevelUpTimeTo;
    [SerializeField] private TextMeshProUGUI textPlaneTimeTo;    

    [SerializeField] private TextMeshProUGUI TextScoreFlash;

    private float tScoreFlash = 0f;
    private float timeToScoreFlash = 0.5f;	
    private float scoreNew;
    
    [Header("Play: New Score")]
    [SerializeField] private GameObject ScoreGroup;	
    [SerializeField] private TextMeshProUGUI textScoreLabels;	
    [SerializeField] private TextMeshProUGUI textScoreNumbers;
    [SerializeField] private TextMeshProUGUI textScoreMaths;


    #region MONOBEHAVIOURS EVENTS ***********************************************************

    private void OnEnable() 
	{            
        // State Playing
		Events.OnPlayingTick.AddListener(OnPlayingTick);

        // State PlaneDone
        Events.OnPlaneDoneEnter.AddListener(OnPlaneDoneEnter);
        Events.OnPlaneDoneTick.AddListener(OnPlaneDoneTick);                
        
        Events.OnSetScore.AddListener(SetScore);	
        Events.OnPlaneDone.AddListener(OnPlaneDone);        
		Events.OnPlaneDoneAndCleaned.AddListener(ExplosionActivate);

        //Events.OnNewScore.AddListener(OnNew);
	}

	private void OnDisable() 
	{
       	Events.OnPlayingTick.RemoveListener(OnPlayingTick);	

        Events.OnPlaneDoneEnter.RemoveListener(OnPlaneDoneEnter);
        Events.OnPlaneDoneTick.RemoveListener(OnPlaneDoneTick);             
        
        Events.OnSetScore.RemoveListener(SetScore);	       
        //
        Events.OnPlaneDone.RemoveListener(OnPlaneDone);        
        Events.OnPlaneDoneAndCleaned.RemoveListener(ExplosionActivate);
	}     
    #endregion 

    #region EVENTS LISTENERS ************************************************************

    public void OnPlaneDone()
    {
        CancelInvoke();
        
        ScoreGroup.SetActive(true);               
        if(gameData.planesExtra > 0)
            textScoreLabels.text = "Level\nPlanes\n   Extras\n";            
        else
            textScoreLabels.text = "Level\nPlanes\n";            
        if(gameData.Coins > 0)
            textScoreLabels.text += "Coins\n";
        textScoreLabels.text += "Blocks";
        
        textScoreNumbers.text =       $"{gameData.Level}\n{gameData.planesDoneAtOnce}\n";
        if(gameData.planesExtra > 0)
             textScoreNumbers.text += $"{gameData.planesExtra}\n";
        if(gameData.Coins > 0)
            textScoreNumbers.text +=  $"{gameData.Coins}\n";
        textScoreNumbers.text += $"{gameData.Blocks}";

        textScoreMaths.text = $"{gameData.CurrentLevel.LevelScoreMultiplier} x\n{gameData.planesDoneAtOnce*gameData.planesDoneAtOnce} x\n";
        if(gameData.planesExtra > 0)
            textScoreMaths.text += $"{gameData.planesExtra*gameData.planesExtra} x\n";
        if(gameData.Coins > 0)
            textScoreMaths.text += $"{gameData.CoinsPoints}\n";
        textScoreMaths.text += $"{gameData.BlocksPoints}\n" + string.Format("{0:N0}",gameData.ScoreNew);
        
        Invoke("HideNewScore", 5);        
    }
    public void HideNewScore()
    {
        ScoreGroup.SetActive(false);
    }
    public void OnPlayingTick() 
    {
        textLevelUpTimeTo.text = string.Format("{0:0}:{1:00}", gameData.LevelUpTimeTo / 60, gameData.LevelUpTimeTo % 60);
        textPlaneTimeTo.text = string.Format("{0:0}:{1:00}", gameData.PlaneTimeTo / 60, gameData.PlaneTimeTo % 60);
        textLevel.text = string.Format("{0:0}", gameData.Level);
        if(gameData.PlanesToDo >= 0)
            textPlanesToDo.text = string.Format("{0:0}", gameData.PlanesToDo);        
        else
            textPlanesToDo.text = string.Format("Ex {0:0}", -gameData.PlanesToDo);        
        textLives.text = string.Format("{0:0}", gameData.Lives);  
        textScore.text = string.Format("{0:N0}", gameData.Score);  
    }

    public void OnPlaneDoneTick()
    {
        Rect rect;

		tScoreFlash += Time.deltaTime;

		TextScoreFlash.transform.localScale = Vector3.Lerp (new Vector3(0.4f, 0.4f, 0f), new Vector3(4f, 4f, 0f), tScoreFlash);
		rect = TextScoreFlash.canvas.GetComponent<RectTransform>().rect;
		TextScoreFlash.transform.position = new Vector3(rect.width / 2f - TextScoreFlash.rectTransform.rect.width / 2f * TextScoreFlash.transform.localScale.x , Mathf.Lerp (rect.height / 2f - 80f, rect.height / 2f + 300f, tScoreFlash), 
														0f);
		//Debug.Log("rect" +  rect.width / 2f);
		TextScoreFlash.color = new Color(255f, 255f, 255f, Mathf.Lerp (255f, 0f, tScoreFlash / timeToScoreFlash));
		//if(TextScoreFlash.transform.localScale == new Vector3(4f, 4f, 0f)) 
        if(tScoreFlash >= timeToScoreFlash) 
        {
			//state = stateEnum.checkingLevels;            
			Conditions.IsPlaneDone = false;
			TextScoreFlash.enabled = false;
			
		}
    }

    public void OnPlaneDoneEnter()
    {
        ExplosionActivate();
        tScoreFlash = 0f;
        //scoreNew = gameController.GetComponent<GameData>().ScoreNew;
        scoreNew = gameData.ScoreNew;
        TextScoreFlash.text = string.Format("+{0:N0}", scoreNew);
        TextScoreFlash.transform.localScale = new Vector3(0f, 0f, 0f);
        TextScoreFlash.enabled = true;         		                
    } 

    public void SetScore(long score)
    {
        textScore.text = string.Format("{0:N0}", score);
    }

    #endregion

    private void ExplosionActivate()
    {
        Explosion.SetActive(false);	 
        Explosion.SetActive(true);	        	    
    }
}

}