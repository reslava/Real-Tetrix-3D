// Add System.IO to work with files!
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class GameDataManager : Singleton<GameDataManager>
{    
    string saveFile;

    public GameData gameData;    

    void Start()
    {     
        saveFile = Application.persistentDataPath + "/gamedata.json";
    }
    
    public bool IsSaved()
    {
        return File.Exists(Application.persistentDataPath + "/gamedata.json");
    }    
    
    public void ReadFile()
    {        
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.                                   
            JsonUtility.FromJsonOverwrite(fileContents, gameData); 
        }
    }

    public void WriteFile()
    {
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(gameData);

        // Write JSON to file.
        File.WriteAllText(saveFile, jsonString);
    }    
    
}

}