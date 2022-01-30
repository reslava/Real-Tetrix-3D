using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

[System.Serializable]
public struct PieceAvailable
{    
    [SerializeField] private Piece _piece; 
    [SerializeField] private int _chance;
    
    public Piece Piece { get => _piece; }
    public int Chance { get => _chance; }
}

[CreateAssetMenu(fileName="Level" ,menuName="Tetrix/Levels")]
public class LevelData : ScriptableObject
{               
    
    [Header("*** Game Level")]
    [SerializeField] private int level;
    [SerializeField] private int levelScoreMultiplier;
    [SerializeField] private int size;  // Size of grid-board  
    [SerializeField] private int planesToDo;	    

    [Tooltip("Max time to complete the level")]
    [SerializeField] private int levelUpMaxTimeTo; 
    
    [Tooltip("Max time to fill next plane")]
    [SerializeField] private int planeMaxTimeTo;	           
    
    [Header("*** Extra info")]
    [SerializeField] private string levelName;	           
    [SerializeField] private string description;	           

    [Header("*** Pieces availables")]
    [SerializeField] private List<PieceAvailable> _piecesAvailables;	               
    public List<PieceAvailable> PiecesAvailables { get => _piecesAvailables; }

    // [Header("*** Blocks availables")]
    // [SerializeField] private GameObject[] pieces;	                       	               
    
    //public int Level { get { return level; } private set { level = value; } }
    public int Level { get { return level; } }
    public int LevelScoreMultiplier { get { return levelScoreMultiplier; } }
    public int Size { get { return size; } }
    public int PlanesToDo { get { return planesToDo; } }
    public int LevelUpMaxTimeTo { get { return levelUpMaxTimeTo; } }
    public int PlaneMaxTimeTo { get { return planeMaxTimeTo; } }
    public string Name { get { return levelName; } }
    public string Description { get { return description; } }
    // public GameObject[] Pieces { get { return pieces; } }

    public Piece RandomPiece()
    {
        var randomIndex = Choose(piecesProbs());
        return _piecesAvailables[randomIndex].Piece;
    }

    private int[] piecesProbs()
    {
        int[] probs = new int[_piecesAvailables.Count];
        var index = 0;
        
        foreach(var p in _piecesAvailables)
        {
            probs[index] = p.Chance;
            index++;
        }
        return probs;
    }

    private int Choose (int[] probs) 
    {
        int total = 0;

        foreach (int elem in probs) {
            total += elem;
        }

        int randomPoint = Random.Range(1, total);

        for (int i= 0; i < probs.Length; i++) 
            if (randomPoint < probs[i]) 
                return i;            
            else 
                randomPoint -= probs[i];
            
        
        return probs.Length - 1;
    }    

// //     float Choose (float[] probs) {
// //     float total = 0;
// //     foreach (float elem in probs) {
// //         total += elem;
// //     }
// //     float randomPoint = Random.value * total;
// //     for (int i= 0; i < probs.Length; i++) {
// //         if (randomPoint < probs[i]) {
// //             return i;
// //         }
// //         else {
// //             randomPoint -= probs[i];
// //         }
// //     }
// //     return probs.Length - 1;
// // }
}

}
