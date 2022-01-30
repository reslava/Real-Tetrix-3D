// Add System.IO to work with files!
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class GameDataRecordsManager : Singleton<GameDataRecordsManager>
{
    // Create a field for the save file.
    string saveFile;
    // Create a GameData field.
    public GameDataRecords gameDataRecords;   

    private void OnEnable() 
    {
        Events.OnLevelHighRecord.AddListener(OnLevelHighRecord);    
        Events.OnScoreRecord.AddListener(WriteFile);    
        Events.OnScoreByLevelRecord.AddListener(OnScoreByLevelRecord);    
    } 

    private void OnDisable() 
    {
        Events.OnLevelHighRecord.RemoveListener(OnLevelHighRecord);    
        Events.OnScoreRecord.RemoveListener(WriteFile);    
        Events.OnScoreByLevelRecord.RemoveListener(OnScoreByLevelRecord);            
    }

    void Start()
    {
        //gameDataRecords = gameObject.GetComponent<GameData>().GetGameDataRecords();
        // Update the path once the persistent path exists.
        saveFile = Application.persistentDataPath + "/gamerecords.json";
    }

    public void ReadFile()
    {
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.                                   
            JsonUtility.FromJsonOverwrite(fileContents, gameDataRecords); 
        }
    }

    public void WriteFile()
    {
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(gameDataRecords);

        // Write JSON to file.
        File.WriteAllText(saveFile, jsonString);
    }

    public void OnScoreByLevelRecord(int level)
    {
        WriteFile();
    }

    public void OnLevelHighRecord(int level)
    {
        WriteFile();
    }
}

}