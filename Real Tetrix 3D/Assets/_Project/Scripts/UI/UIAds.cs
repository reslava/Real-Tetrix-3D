using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RafaEslava {

public class UIAds : Singleton<UIAds>
{
    [Header("Canvas")]    
    [SerializeField] private Canvas CanvasPlay;	
    [SerializeField] private Canvas CanvasReward;    
    
    [Header("Ads")]    
    [SerializeField] private Button buttonAdsReward;  

    #region MONOBEHAVIOURS EVENTS *******************************************************

    private void OnEnable() 
	{            
        // Ads        
        Events.OnRewardedLoaded.AddListener(OnRewardedLoaded);
        Events.OnInterstitialLoaded.AddListener(OnInterstitialLoaded);
        Events.OnAdsReward.AddListener(OnAdsReward);		
        Events.OnInterstitialShowComplete.AddListener(OnInterstitialShowComplete);
    }

    private void OnDisable() 
	{
        // Ads        
        Events.OnRewardedLoaded.RemoveListener(OnRewardedLoaded);
        Events.OnInterstitialLoaded.RemoveListener(OnInterstitialLoaded);
        Events.OnAdsReward.RemoveListener(OnAdsReward);		
        Events.OnInterstitialShowComplete.RemoveListener(OnInterstitialShowComplete);
    }

    #endregion

    #region PUBLIC METHODS *******************************************************

    public void OnInterstitialShowComplete()
    {
        Conditions.IsGameOver = false;
    }

    public void OnAdsReward()
    {
        Invoke("OnAdsReward2", 0.05f);
    }

    public void OnAdsReward2()
    {
        //Tools.IsPaused = true;
        CanvasGameObjectsDisable();        		
        CanvasReward.gameObject.SetActive (true);	
    }

    private void OnRewardedLoaded(bool isLoaded)
    {   
        buttonAdsReward.gameObject.SetActive(isLoaded);
    }

    private void OnInterstitialLoaded(bool isLoaded)
    {

    }

    public void OnAdsRewardFinished()
    {
        //Tools.IsPaused = false;
        CanvasGameObjectsDisable();        		
        CanvasPlay.gameObject.SetActive (true);	
    }

    #endregion    

    private void CanvasGameObjectsDisable() 
    {
        foreach(Transform transform in gameObject.transform)
            transform.GetComponent<Canvas>().gameObject.SetActive(false);		
    }
}

}