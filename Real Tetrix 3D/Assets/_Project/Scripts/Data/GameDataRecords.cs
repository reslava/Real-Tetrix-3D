using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava
{
    [CreateAssetMenu(fileName="GameDataRecords" ,menuName="Tetrix/Records")]
    public class GameDataRecords : ScriptableObject
    {        
        // All time statistics    
        public long ScoreHigh;    
        public int LevelHigh;
        public long[] ScoresHighByLevel;
        
        public void Initialize() 
        {            
            ScoreHigh = 0;
            LevelHigh = 3;             
        }

        private GameDataRecords()
        {
            Initialize();
        }             
    }
}
