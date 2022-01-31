#if UNITY_ANDROID     
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace RafaEslava {   
public class GPS : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    private  static PlayGamesPlatform _platform;        
    private string[] _leaderBoardsByLevel;
    private string[] _eventsLevelsUnlocked;
    
    private void Start()
    {
        if (_platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            _platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("GPS*** Logged in successfully");
                LeaderBoardsByeLevelInitialize();
            }
            else
            {
                Debug.Log("GPS*** Login Failed");
            }
        });            
    }

    private void OnEnable() 
    {
        Events.OnLevelHighRecord.AddListener(OnLevelHighRecord);    
        Events.OnScoreRecord.AddListener(OnScoreRecord);    
        Events.OnScoreByLevelRecord.AddListener(OnScoreByLevelRecord);   
        Events.OnPlaneDone.AddListener(OnPlaneDone);
        Events.OnPlanesDoneAtOnce.AddListener(OnPlanesDoneAtOnce);         
    } 

    private void OnDisable() 
    {
        Events.OnLevelHighRecord.RemoveListener(OnLevelHighRecord);    
        Events.OnScoreRecord.RemoveListener(OnScoreRecord);    
        Events.OnScoreByLevelRecord.RemoveListener(OnScoreByLevelRecord);            
        Events.OnPlaneDone.RemoveListener(OnPlaneDone);
        Events.OnPlanesDoneAtOnce.RemoveListener(OnPlanesDoneAtOnce);        
    }


    private void LeaderBoardsByeLevelInitialize()
    {  

        _leaderBoardsByLevel = new string[_gameData.LevelsData.Length];
        _leaderBoardsByLevel[0] = GPGSIds.leaderboard_level_1_highscores;
        _leaderBoardsByLevel[1] = GPGSIds.leaderboard_level_2_highscores;
        _leaderBoardsByLevel[2] = GPGSIds.leaderboard_level_3_highscores;
        _leaderBoardsByLevel[3] = GPGSIds.leaderboard_level_4_highscores;
        _leaderBoardsByLevel[4] = GPGSIds.leaderboard_level_5_highscores;
        _leaderBoardsByLevel[5] = GPGSIds.leaderboard_level_6_highscores;      

        _eventsLevelsUnlocked = new string[_gameData.LevelsData.Length];
        _eventsLevelsUnlocked[3] = GPGSIds.event_level_4_unlocked;
        _eventsLevelsUnlocked[4] = GPGSIds.event_level_5_unlocked;
        _eventsLevelsUnlocked[5] = GPGSIds.event_level_6_unlocked;
        _eventsLevelsUnlocked[6] = GPGSIds.event_level_7_unlocked;
        _eventsLevelsUnlocked[7] = GPGSIds.event_level_8_unlocked;
        _eventsLevelsUnlocked[8] = GPGSIds.event_level_9_unlocked;
        _eventsLevelsUnlocked[9] = GPGSIds.event_level_10_unlocked;
        _eventsLevelsUnlocked[10] = GPGSIds.event_level_11_unlocked;
        _eventsLevelsUnlocked[11] = GPGSIds.event_level_12_unlocked;
        _eventsLevelsUnlocked[12] = GPGSIds.event_level_13_unlocked;
        _eventsLevelsUnlocked[13] = GPGSIds.event_level_14_unlocked;
        _eventsLevelsUnlocked[14] = GPGSIds.event_level_15_unlocked;
        _eventsLevelsUnlocked[15] = GPGSIds.event_level_16_unlocked;
        
    }             

    private void OnPlaneDone()
    {
        if(Social.Active.localUser.authenticated)
        {                
            Debug.Log("GPS*** On plane done");
            
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_first_30_planes, 1, (success) =>
            {
                if(!success) Debug.Log("GPS*** error sending achievement_first_30_planes");
            });                    
            
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_first_100_planes, 1, (success) =>
            {
                if(!success) Debug.Log("GPS*** error sending achievement_first_100_planes");
            });                    
        }
        UnlockPointsAchievement();
    }
    
    private void OnPlanesDoneAtOnce(int planes)
    {
        if(Social.Active.localUser.authenticated)
        {   
            Debug.Log($"GPS*** On plane at once: {planes}");

            if(planes == 2)             
            {                
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_first_10_sets_of_2_planes_at_once, 1, (success) =>
                {
                    if(!success) Debug.Log("GPS*** ERROR achievement_first_10_sets_of_2_planes_at_once");
                });              
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_first_30_sets_of_2_planes_at_once, 1, (success) =>
                {
                    if(!success) Debug.Log("GPS*** ERROR achievement_first_30_sets_of_2_planes_at_once");
                });                    
            }                
            if(planes == 3)             
            {                
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_first_5_sets_of_3_planes_at_once, 1, (success) =>
                {
                    if(!success) Debug.Log("GPS*** ERROR achievement_first_5_sets_of_3_planes_at_once");
                });                 
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_first_15_sets_of_3_planes_at_once, 1, (success) =>
                {
                    if(!success) Debug.Log("GPS*** ERROR achievement_first_15_sets_of_3_planes_at_once");
                });                    
            }
            if(planes == 4)             
            {                
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_first_2_sets_of_4_planes_at_once, 1, (success) =>
                {
                    if(!success) Debug.Log("GPS*** ERROR achievement_first_2_sets_of_4_planes_at_once");
                });                 
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_first_3_sets_of_4_planes_at_a_time, 1, (success) =>
                {
                    if(!success) Debug.Log("GPS*** ERROR achievement_first_3_sets_of_4_planes_at_a_time");
                });                    
            }
        }
    }

    public void OnScoreRecord()
    {
        if(Social.Active.localUser.authenticated)
        {                
            Debug.Log($"GPS*** On score record: {_gameData.Score}");

            Social.ReportScore(_gameData.Score, GPGSIds.leaderboard_best_scores, (success) =>
            {
                if(!success) Debug.Log("GPS*** ERROR sending score record");
            });                    
        }
    }

    public void OnScoreByLevelRecord(int level)
    {
        if(Social.Active.localUser.authenticated)
        {                
            Debug.Log("GPS*** On score record by level {level}: {_gameData.Score}");

            Social.ReportScore(_gameData.Score, _leaderBoardsByLevel[level -1], (success) =>
            {
                if(!success) Debug.Log("GPS*** ERROR sending score record by level");
            });                    
        }
    }
    public void OnLevelHighRecord(int level)
    {
        PlayGamesPlatform.Instance.Events.IncrementEvent(_eventsLevelsUnlocked[level], 1);
    }

    public void ShowLeaderBoards()
    {
        if(Social.Active.localUser.authenticated)
            Social.ShowLeaderboardUI();
    }
    
    public void ShowMainLeaderBoard()
    {
        if(Social.Active.localUser.authenticated)
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_best_scores);
    }

    public void ShowCurrentLevelLeaderBoard()
    {
        if(Social.Active.localUser.authenticated)
            PlayGamesPlatform.Instance.ShowLeaderboardUI(_leaderBoardsByLevel[_gameData.Level - 1]);
    }

    public void ShowLeaderBoardByLevel(int level)
    {
        if(Social.Active.localUser.authenticated)
            PlayGamesPlatform.Instance.ShowLeaderboardUI(_leaderBoardsByLevel[level - 1]);
    }

    public void ShowAchievements()
    {
        if(Social.Active.localUser.authenticated)
            Social.ShowAchievementsUI();
    }

    public void UnlockPointsAchievement()
    {
        long score = _gameData.Score;
        
        if(score >= 1000)
        {
            Social.ReportProgress(GPGSIds.achievement_1000_points, 100f, success => { });                
            if(score >= 5000)        
            {
                Social.ReportProgress(GPGSIds.achievement_5000_points, 100f, success => { });        
                if(score >= 10000)        
                {
                    Social.ReportProgress(GPGSIds.achievement_10000_points, 100f, success => { });        
                    if(score >= 50000)        
                    {
                        Social.ReportProgress(GPGSIds.achievement_50000_points, 100f, success => { });        
                        if(score >= 100000)        
                            Social.ReportProgress(GPGSIds.achievement_100000_points, 100f, success => { });
                    }
                }
            }
        }
    }
}
}
#endif